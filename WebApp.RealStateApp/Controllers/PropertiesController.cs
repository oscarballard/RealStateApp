using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyImprovements;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealStateApp.Controllers
{
    //[Authorize(Roles = "Agent")]
    public class PropertiesController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyImprovementsService _propertyImprovementsService;
        private readonly IPropertyService _propertyService;
        private readonly ISalesTypeService _salesTypeService;
        private readonly IImprovementsService _improvementsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public PropertiesController(IPropertyService propertyService, IPropertyImprovementsService propertyImprovementsService,ISalesTypeService salesTypeService, IPropertyTypeService propertyTypeService, IImprovementsService improvementsService, IHttpContextAccessor httpContextAccessor)
        {
            _propertyTypeService = propertyTypeService;
            _salesTypeService = salesTypeService;
            _improvementsService = improvementsService;
            _propertyImprovementsService = propertyImprovementsService;
            _propertyService = propertyService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<IActionResult> Index(FilterPropertyViewModel vm)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            return View(await _propertyService.GetAllViewModelWithFilters(vm));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            ViewBag.SalesType = await _salesTypeService.GetAllViewModel();
            ViewBag.Mejoras = await _improvementsService.GetAllViewModel();

            SavePropertyViewModel vm = new SavePropertyViewModel();
            vm.Mejoras = ViewBag.Mejoras;

            return View("SaveProperty", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropertyViewModel vm)
        {
            SavePropertyViewModel propertyVm = await _propertyService.Add(vm);
            propertyVm.Codigo = propertyVm.Id.ToString().PadLeft(6, '0');
            propertyVm.IdAgente = userViewModel.Id;
            if (propertyVm.Id != 0 && propertyVm != null)
            {
                propertyVm.Imagen1 = UploadFile(vm.FileImagen1, propertyVm.Id);
                propertyVm.Imagen2 = UploadFile(vm.FileImagen2, propertyVm.Id);
                propertyVm.Imagen3 = UploadFile(vm.FileImagen3, propertyVm.Id);
                propertyVm.Imagen4 = UploadFile(vm.FileImagen4, propertyVm.Id);
            }

            await _propertyService.Update(propertyVm, propertyVm.Id);

            SavePropertyImprovementsViewModel mejoraVm = new();
            mejoraVm.IdPropiedad = propertyVm.Id;

            foreach (var Mejora in vm.Mejoras)
            {
                mejoraVm.IdMejora = Mejora.Id;
                await _propertyImprovementsService.Add(mejoraVm);
            }

            return RedirectToAction("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            ViewBag.SalesType = await _salesTypeService.GetAllViewModel();
            ViewBag.Mejoras = await _improvementsService.GetAllViewModel();
            SavePropertyViewModel vm = await _propertyService.GetByIdSaveViewModel(Id);
            return View("SaveProperty", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SavePropertyViewModel vm)
        {
            await _propertyService.Update(vm, vm.Id);
            return RedirectToAction("Index");
        }

        public string GenerateSequence(int Id)
        {
            var sequence = Id.ToString().PadLeft(9, '0');
            return sequence;
        }
        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Properties/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}

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
    [Authorize(Roles = "Agent")]
    public class PropertiesController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyImprovementsService _propertyImprovementsService;
        private readonly IPropertyService _propertyService;
        private readonly ISalesTypeService _salesTypeService;
        private readonly IImprovementsService _improvementsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsersViewModel userViewModel;
        public PropertiesController(IPropertyService propertyService, IPropertyImprovementsService propertyImprovementsService,ISalesTypeService salesTypeService, IPropertyTypeService propertyTypeService, IImprovementsService improvementsService, IHttpContextAccessor httpContextAccessor)
        {
            _propertyTypeService = propertyTypeService;
            _salesTypeService = salesTypeService;
            _improvementsService = improvementsService;
            _propertyImprovementsService = propertyImprovementsService;
            _propertyService = propertyService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsersViewModel>("user");
        }

        public async Task<IActionResult> Index(FilterPropertyViewModel vm)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            return View(await _propertyService.GetAllViewModelWithFilters(vm));
        }

        public async Task<IActionResult> FavoryProperties(FilterPropertyViewModel vm)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            vm.IdClient = userViewModel.Id;
            return View(await _propertyService.GetAllViewModelWithFilters(vm));
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            ViewBag.SalesType = await _salesTypeService.GetAllViewModel();
            ViewBag.Mejoras = await _improvementsService.GetAllViewModel();

            SavePropertyViewModel vm = new SavePropertyViewModel();

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
                if (vm.FileImagen1 != null)
                {
                    propertyVm.Imagen1 = UploadFile(vm.FileImagen1, propertyVm.Id);
                }
                if (vm.FileImagen2 != null)
                {
                    propertyVm.Imagen2 = UploadFile(vm.FileImagen2, propertyVm.Id);
                }
                if (vm.FileImagen3 != null)
                {
                    propertyVm.Imagen3 = UploadFile(vm.FileImagen3, propertyVm.Id);
                }
                if (vm.FileImagen4 != null)
                {
                    propertyVm.Imagen4 = UploadFile(vm.FileImagen4, propertyVm.Id);
                }
                await _propertyService.Update(propertyVm, propertyVm.Id);
            }

            SavePropertyImprovementsViewModel mejoraVm = new();
            mejoraVm.IdPropiedad = propertyVm.Id;

            await _propertyImprovementsService.DeleteAllAsync(propertyVm.Id);
            foreach (var Mejora in vm.MejorasId)
            {
                mejoraVm.IdMejora = Mejora;
                await _propertyImprovementsService.Add(mejoraVm);
            }

            return RedirectToAction("Index");
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
            await _propertyImprovementsService.DeleteAllAsync(vm.Id);
            SavePropertyImprovementsViewModel mejoraVm = new();
            mejoraVm.IdPropiedad = vm.Id;
            foreach (var Mejora in vm.MejorasId)
            {
                mejoraVm.IdMejora = Mejora;
                await _propertyImprovementsService.Add(mejoraVm);
            }

            SavePropertyViewModel propertyVm = await _propertyService.GetByIdSaveViewModel(vm.Id);

            if (propertyVm.Imagen1 != null)
            {
                if (vm.FileImagen1 != null)
                {
                    vm.Imagen1 = UploadFile(vm.FileImagen1, propertyVm.Id, true, propertyVm.Imagen1);
                }
                else
                {
                    vm.Imagen1 = propertyVm.Imagen1;
                }
            }
            else 
            {
                if (vm.FileImagen1 != null)
                {
                    vm.Imagen1 = UploadFile(vm.FileImagen1, propertyVm.Id);
                }
            }

            if (propertyVm.Imagen2 != null)
            {
                
                if (vm.FileImagen2 != null)
                {
                    vm.Imagen2 = UploadFile(vm.FileImagen2, propertyVm.Id, true, propertyVm.Imagen2);
                }
                else
                {
                    vm.Imagen2 = propertyVm.Imagen2;
                }
            }
            else
            {
                if (vm.FileImagen2 != null)
                {
                    vm.Imagen2 = UploadFile(vm.FileImagen2, propertyVm.Id);
                }
                else
                {
                    vm.Imagen2 = propertyVm.Imagen2;
                }
            }

            if (propertyVm.Imagen3 != null)
            {
               
                if (vm.FileImagen3 != null)
                {
                    vm.Imagen3 = UploadFile(vm.FileImagen3, propertyVm.Id, true, propertyVm.Imagen3);
                }
                else
                {
                    vm.Imagen3 = propertyVm.Imagen3;
                }
            }
            else
            {
                if (vm.FileImagen3 != null)
                {
                    vm.Imagen3 = UploadFile(vm.FileImagen3, propertyVm.Id);
                }
            }

            if (propertyVm.Imagen4 != null)
            {
                if (vm.FileImagen4 != null)
                {
                    vm.Imagen4 = UploadFile(vm.FileImagen4, propertyVm.Id, true, propertyVm.Imagen4);

                }
                else
                {
                    vm.Imagen4= propertyVm.Imagen4;
                }
            }
            else
            {
                if (vm.FileImagen4 != null)
                {
                    vm.Imagen4 = UploadFile(vm.FileImagen4, propertyVm.Id);
                }
            }

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

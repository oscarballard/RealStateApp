using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealStateApp.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyImprovementsService _propertyImprovementsService;
        private readonly IPropertyService _propertyService;
        private readonly ISalesTypeService _salesTypeService;
        private readonly IImprovementsService _improvementsService;
        public PropertiesController(IPropertyService propertyService, IPropertyImprovementsService propertyImprovementsService,ISalesTypeService salesTypeService, IPropertyTypeService propertyTypeService, IImprovementsService improvementsService)
        {
            _propertyTypeService = propertyTypeService;
            _salesTypeService = salesTypeService;
            _improvementsService = improvementsService;
            _propertyImprovementsService = propertyImprovementsService;
            _propertyService = propertyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            ViewBag.SalesType = await _salesTypeService.GetAllViewModel();
            ViewBag.Mejoras = await _improvementsService.GetAllViewModel();

            return View("SaveProperty", new SavePropertyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropertyViewModel vm)
        {
            SavePropertyViewModel propertyVm = await _propertyService.Add(vm);
            propertyVm.Codigo = propertyVm.Id.ToString().PadLeft(6, '0');
            if (propertyVm.Id != 0 && propertyVm != null)
            {
                propertyVm.Imagen1 = UploadFile(vm.FileImagen1, propertyVm.Id);
                propertyVm.Imagen2 = UploadFile(vm.FileImagen2, propertyVm.Id);
                propertyVm.Imagen3 = UploadFile(vm.FileImagen3, propertyVm.Id);
                propertyVm.Imagen4 = UploadFile(vm.FileImagen4, propertyVm.Id);
            }
            await _propertyService.Update(propertyVm, propertyVm.Id);

            return RedirectToAction("Create");
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

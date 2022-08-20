using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealStateApp.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISalesTypeService _salesTypeService;
        private readonly IImprovementsService _improvementsService;
        public PropertiesController(ISalesTypeService salesTypeService, IPropertyTypeService propertyTypeService, IImprovementsService improvementsService)
        {
            _propertyTypeService = propertyTypeService;
            _salesTypeService = salesTypeService;
            _improvementsService = improvementsService;
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

            return View("SaveProperty",new SavePropertyViewModel());
        }
    }
}

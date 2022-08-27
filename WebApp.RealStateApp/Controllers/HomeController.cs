﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RealStateApp.Models;

namespace WebApp.RealStateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IMapper _mapper;
        public HomeController(IMapper mapper,IPropertyTypeService propertyTypeService,IPropertyService propertyService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(FilterPropertyViewModel vm)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            return View(await _propertyService.GetAllViewModelWithFilters(vm));
        }
        public async Task<IActionResult> IndexByCode(FilterPropertyViewModel vm)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            List<PropertyViewModel> propiedad = new List<PropertyViewModel>();
            propiedad.Add(await _propertyService.GetByCodeViewModel(vm.Codigo));
            return View("Index", propiedad);
        }
        public async Task<IActionResult> Detalle(int Id)
        {
            PropertyViewModel vm = await _propertyService.GetByIdViewModel(Id);
            return View("PropertyDetail",vm);
        }

        public IActionResult indexAdmin()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

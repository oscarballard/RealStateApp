﻿using Microsoft.AspNetCore.Mvc;
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
        public HomeController(IPropertyService propertyService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index(FilterPropertyViewModel vm)
        {
            return View(await _propertyService.GetAllViewModelWithFilters(vm));
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

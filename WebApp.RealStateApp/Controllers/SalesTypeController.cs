using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.SalesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RealStateApp.Middlewares;

namespace WebApp.RealStateApp.Controllers
{
    public class SalesTypeController : Controller
    {
        private readonly ISalesTypeService _propertyTypeService;
        private readonly ValidateUserSession _validateUserSession;
        public SalesTypeController(ValidateUserSession validateUserSession, ISalesTypeService propertyTypeService)
        {
            _validateUserSession = validateUserSession;
            _propertyTypeService = propertyTypeService;
        }
        public async Task<IActionResult> Index()
        {
            //if (!_validateUserSession.HasUser())
            //{
            //    return RedirectToRoute(new { controller = "Home", action = "Index" });
            //}
            ViewBag.Tipos = await _propertyTypeService.GetAllWithIncludes();
            return View();
        }

        public IActionResult Create()
        {
            return View("SaveSalesType", new SaveSalesTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSalesTypeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveSalesType", vm);
            }
            await _propertyTypeService.Add(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            return View("SaveSalesType", await _propertyTypeService.GetByIdSaveViewModel(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveSalesTypeViewModel vm)
        {
            await _propertyTypeService.Update(vm,vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _propertyTypeService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _propertyTypeService.Delete(id);

            return RedirectToRoute(new { controller = "SalesType", action = "Index" });
        }
    }
}

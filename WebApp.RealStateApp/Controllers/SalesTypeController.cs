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
        private readonly ISalesTypeService _salesTypeService;
        private readonly ValidateUserSession _validateUserSession;
        public SalesTypeController(ValidateUserSession validateUserSession, ISalesTypeService propertyTypeService)
        {
            _validateUserSession = validateUserSession;
            _salesTypeService = propertyTypeService;
        }
        public async Task<IActionResult> Index()
        {
            //if (!_validateUserSession.HasUser())
            //{
            //    return RedirectToRoute(new { controller = "Home", action = "Index" });
            //}
            ViewBag.Tipos = await _salesTypeService.GetAllWithIncludes();
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
            await _salesTypeService.Add(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            return View("SaveSalesType", await _salesTypeService.GetByIdSaveViewModel(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveSalesTypeViewModel vm)
        {
            await _salesTypeService.Update(vm,vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _salesTypeService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _salesTypeService.Delete(id);

            return RedirectToRoute(new { controller = "SalesType", action = "Index" });
        }
    }
}

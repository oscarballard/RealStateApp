using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Improvements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RealStateApp.Middlewares;

namespace WebApp.RealStateApp.Controllers
{
    public class ImprovementsController : Controller
    {
        private readonly IImprovementsService _propertyTypeService;
        private readonly ValidateUserSession _validateUserSession;
        public ImprovementsController(ValidateUserSession validateUserSession, IImprovementsService propertyTypeService)
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
            return View("SaveImprovements", new SaveImprovementsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveImprovementsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveImprovements", vm);
            }
            await _propertyTypeService.Add(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            return View("SaveImprovements", await _propertyTypeService.GetByIdSaveViewModel(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveImprovementsViewModel vm)
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

            return RedirectToRoute(new { controller = "Improvements", action = "Index" });
        }
    }
}

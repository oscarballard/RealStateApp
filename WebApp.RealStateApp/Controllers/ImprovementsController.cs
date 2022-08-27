using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class ImprovementsController : Controller
    {
        private readonly IImprovementsService _ImprovementsService;
        private readonly ValidateUserSession _validateUserSession;
        public ImprovementsController(ValidateUserSession validateUserSession, IImprovementsService ImprovementsService)
        {
            _validateUserSession = validateUserSession;
            _ImprovementsService = ImprovementsService;
        }
        public async Task<IActionResult> Index()
        {
            //if (!_validateUserSession.HasUser())
            //{
            //    return RedirectToRoute(new { controller = "Home", action = "Index" });
            //}
            ViewBag.Tipos = await _ImprovementsService.GetAllWithIncludes();
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
            await _ImprovementsService.Add(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            return View("SaveImprovements", await _ImprovementsService.GetByIdSaveViewModel(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveImprovementsViewModel vm)
        {
            await _ImprovementsService.Update(vm,vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _ImprovementsService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _ImprovementsService.Delete(id);

            return RedirectToRoute(new { controller = "Improvements", action = "Index" });
        }
    }
}

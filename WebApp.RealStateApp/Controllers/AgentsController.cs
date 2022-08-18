using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RealStateApp.Core.Application.Helpers;
using AutoMapper;
using RealStateApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using WebApp.RealStateApp.Middlewares;

namespace WebApp.RealStateApp.Controllers
{
    public class AgentsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IMapper _mapper;

        public AgentsController(IUserService userService, ValidateUserSession validateUserSession, IMapper mapper)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            ViewBag.Mensaje = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            UserViewModel userVm = await _userService.LoginAsync(vm);

            if (userVm != null)
            {
                if (userVm.State == 0)
                {
                    ModelState.AddModelError("userValidation", "El usuario esta inactivo. Verifique su correo");
                    return View(vm);
                }

                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos incorrectos");
            }
            return View(vm);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Agents", action = "Index" });
        }
        public IActionResult Create()
        {
            ViewBag.Existe = false;
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Agents", action = "Index" });
            }
            return View("SaveAgents", new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {

            var userCreated = await _userService.GetUserName(vm.Username);
            if (!ModelState.IsValid || userCreated != null)
            {
                ViewBag.Existe = userCreated != null ? true : false;
                return View("SaveAgents", vm);
            }
            ViewBag.Existe = false;

            SaveUserViewModel userVm = await _userService.Add(vm);

            if (userVm != null && userVm.Id != 0)
            {
                userVm.Photo = UploadFile(vm.File, userVm.Id);
                await _userService.Update(userVm, userVm.Id);
            }

            return RedirectToRoute(new { controller = "Agents", action = "Index" });
        }
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveAgents", await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Agents", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveAgents", vm);
            }

            await _userService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "Agents", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Agents", action = "Index" });
            }
            await _userService.Delete(id);
            return RedirectToRoute(new { controller = "Agents", action = "Index" });
        }
        [HttpGet]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var user = await _userService.GetByIdSaveViewModel(id);
            SaveUserViewModel vm = _mapper.Map<SaveUserViewModel>(user);
            vm.Estado = 1;
            await _userService.Update(vm, id);
            return View("ActivateUser", vm);
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl = "")
        {
            if (isEditMode && file == null)
            {
                return imageUrl;
            }
            string basePath = $"/Img/Users/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

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
                string[] oldImagePath = imageUrl.Split("/");
                string oldImageName = oldImagePath[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);
                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}

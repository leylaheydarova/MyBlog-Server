using Microsoft.AspNetCore.Mvc;
using MyBlog.App.DTOs.AppUser;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Controllers
{
    public class AppUserController : Controller
    {
        readonly IAppUserService _service;

        public AppUserController(IAppUserService service)
        {
            _service = service;
        }

        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.RegisterAsync(dto);
            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home");
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var result = await _service.LoginAsync(dto);
            if (!result) return NotFound("user");
            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home");
        }

    }
}

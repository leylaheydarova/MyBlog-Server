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

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            if (id == null) NotFound("User not found!");
            return View(await _service.GetSingleAsync((int)id));
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
            HttpContext.Session.SetInt32("UserId", result);
            return RedirectToAction(
                controllerName: "Home",
                actionName: "Index");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            if (id == null) NotFound("User not found!");
            HttpContext.Session.Remove("UserId");
            return RedirectToAction(
                controllerName: "Home",
                actionName: "Index");
        }

        [HttpGet("user/edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var user = await _service.GetSingleAsync(id);
            var updateDto = new AppUserUpdateDto
            {
                Email = user.Email,
                Password = user.Password,
                Username = user.UserName,
            };
            return View(updateDto);
        }

        [HttpPost("user/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, AppUserUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.UpdateAsync(id, dto);
            return RedirectToAction(controllerName: "Home",
                actionName: "Index");
        }

        [HttpPost("user/remove/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAccount([FromRoute] int id)
        {
            if (!ModelState.IsValid) return View();
            await _service.RemoveAccountAsync(id);
            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }
    }
}

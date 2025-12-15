using Microsoft.AspNetCore.Mvc;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Controllers
{
    public class BlogController : Controller
    {
        readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }
    }
}

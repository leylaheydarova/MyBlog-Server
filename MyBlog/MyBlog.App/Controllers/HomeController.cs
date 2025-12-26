using Microsoft.AspNetCore.Mvc;
using MyBlog.App.Models;
using MyBlog.App.Services.Interfaces;
using MyBlog.App.ViewModels.Home;
using System.Diagnostics;

namespace MyBlog.App.Controllers
{
    public class HomeController : Controller
    {
        readonly IBlogService _blogService;
        readonly ICategoryService _categoryService;
        public HomeController(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            var blogs = await _blogService.GetAllAsync();
            var vm = new IndexVM()
            {
                Blogs = blogs,
                Categories = categories
            };
            return View(vm);
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

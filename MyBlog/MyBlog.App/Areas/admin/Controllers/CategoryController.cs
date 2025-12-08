using Microsoft.AspNetCore.Mvc;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }


    }
}

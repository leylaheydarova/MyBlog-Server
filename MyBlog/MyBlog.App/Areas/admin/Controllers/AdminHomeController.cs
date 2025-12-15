using Microsoft.AspNetCore.Mvc;

namespace MyBlog.App.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

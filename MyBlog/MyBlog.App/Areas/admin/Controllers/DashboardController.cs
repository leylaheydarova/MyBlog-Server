using Microsoft.AspNetCore.Mvc;

namespace MyBlog.App.Areas.admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

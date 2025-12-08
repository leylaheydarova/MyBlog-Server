using Microsoft.AspNetCore.Mvc;

namespace MyBlog.App.Areas.admin.Controllers
{
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

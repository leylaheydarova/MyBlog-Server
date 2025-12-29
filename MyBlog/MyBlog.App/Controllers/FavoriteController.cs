using Microsoft.AspNetCore.Mvc;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Controllers
{
    public class FavoriteController : Controller
    {
        readonly IFavoriteItemService _service;

        public FavoriteController(IFavoriteItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;
            if (userId == null) return NotFound("User not found!");
            var list = await _service.GetFavorite(userId);
            return View(list);
        }

        [HttpPost("addToList/{blogId}")]
        public async Task<IActionResult> AddToList([FromRoute] int blogId)
        {
            var userId = HttpContext.Session.GetInt32("UserId").Value;
            if (userId == null) return NotFound("User not found!");
            await _service.CreateAsync(blogId, userId);
            return RedirectToAction(
                controllerName: "Home",
                actionName: "Index");
        }

        [HttpPost("removeFromList/{itemId}")]
        public async Task<IActionResult> RemoveFromList([FromRoute] int itemId)
        {
            await _service.RemoveAsync(itemId);
            return RedirectToAction(nameof(Index));
        }
    }
}

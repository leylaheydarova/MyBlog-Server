using Microsoft.AspNetCore.Mvc;
using MyBlog.App.DTOs.Blog;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Areas.admin.Controllers
{
    [Area("Admin")]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateDto dto)
        {
            if (!ModelState.IsValid) View(dto);

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("blog/edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var getDto = await _service.GetSingleAsync(id);
            var updateDto = new BlogUpdateDto() { Title = getDto.Title, Text = getDto.Text, Id = id };
            return View(updateDto);
        }

        [HttpPost("blog/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, BlogUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("blog/remove/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

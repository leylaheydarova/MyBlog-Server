using Microsoft.AspNetCore.Mvc;
using MyBlog.App.DTOs.Category;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Areas.admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var getDto = await _service.GetSingleAsync(id);
            var updateDto = new CategoryUpdateDto() { Name = getDto.Name, Id = id };
            return View(updateDto);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CategoryUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
    }
}

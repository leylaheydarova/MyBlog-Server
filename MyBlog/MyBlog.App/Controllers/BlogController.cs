using Microsoft.AspNetCore.Mvc;
using MyBlog.App.Services.Interfaces;
using MyBlog.App.ViewModels.Blog;

namespace MyBlog.App.Controllers
{
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;
        readonly ICommentService _commentService;

        public BlogController(IBlogService service, ICommentService commentService)
        {
            _blogService = service;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }

        [HttpGet("blog/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var dto = await _blogService.GetSingleAsync(id);
            var comments = await _commentService.GetAll(id);
            var vm = new BlogGetVM() { BlogGetDto = dto, CommentGetDto = comments };
            return View(vm);
        }
    }
}

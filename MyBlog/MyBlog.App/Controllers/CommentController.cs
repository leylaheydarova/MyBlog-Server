using Microsoft.AspNetCore.Mvc;
using MyBlog.App.DTOs.Comment;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Controllers
{
    public class CommentController : Controller
    {
        readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpPost("comment/{blogId}")]
        public async Task<IActionResult> Add([FromRoute] int blogId, string commentText)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return NotFound("userId not found");
            var dto = new CommentCreateDto()
            {
                BlogId = blogId,
                CommentText = commentText,
                UserId = userId.Value
            };
            await _service.AddComment(dto);
            return RedirectToAction(controllerName: "Blog",
                actionName: "Get",
                routeValues: new { id = blogId });
        }

        [HttpPost("remove-comment/{commentID}")]
        public async Task<IActionResult> Remove([FromRoute] int commentID)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return NotFound("userId not found!");
            var blogId = await _service.RemoveComment(commentID, userId.Value);
            return RedirectToAction(controllerName: "Blog",
                actionName: "Get",
                routeValues: new { id = blogId });
        }
    }
}

using MyBlog.App.DTOs.Blog;
using MyBlog.App.DTOs.Comment;

namespace MyBlog.App.ViewModels.Blog
{
    public class BlogGetVM
    {
        public BlogGetDto BlogGetDto { get; set; }
        public string? CommentText { get; set; }
        public List<CommentGetDto> CommentGetDto { get; set; } = new List<CommentGetDto>();
    }
}

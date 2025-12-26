using MyBlog.App.DTOs.Blog;
using MyBlog.App.DTOs.Category;

namespace MyBlog.App.ViewModels.Home
{
    public class IndexVM
    {
        public List<CategoryGetDto> Categories { get; set; }
        public List<BlogGetDto> Blogs { get; set; }
    }
}

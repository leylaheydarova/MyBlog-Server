using MyBlog.App.Models.BaseModels;

namespace MyBlog.App.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}

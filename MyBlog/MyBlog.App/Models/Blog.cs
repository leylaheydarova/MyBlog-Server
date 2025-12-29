using MyBlog.App.Models.BaseModels;

namespace MyBlog.App.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

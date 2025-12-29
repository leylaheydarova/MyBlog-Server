using MyBlog.App.Models.BaseModels;

namespace MyBlog.App.Models
{
    public class Comment : BaseEntity
    {
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}

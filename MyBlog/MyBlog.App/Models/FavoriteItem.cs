using MyBlog.App.Models.BaseModels;

namespace MyBlog.App.Models
{
    public class FavoriteItem : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int FavoriteId { get; set; }
        public Favorite Favorite { get; set; }
    }
}

using MyBlog.App.Models.BaseModels;

namespace MyBlog.App.Models
{
    public class Favorite : BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<FavoriteItem> FavoriteItems { get; set; } = new List<FavoriteItem>();
    }
}

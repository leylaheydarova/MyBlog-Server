namespace MyBlog.App.DTOs.Favorite
{
    public class FavoriteItemGetDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogText { get; set; }
        public int FavoriteId { get; set; }
    }
}

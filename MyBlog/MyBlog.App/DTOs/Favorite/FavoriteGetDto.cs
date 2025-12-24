namespace MyBlog.App.DTOs.Favorite
{
    public class FavoriteGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<FavoriteItemGetDto> ItemGetDtos { get; set; } = new List<FavoriteItemGetDto>();
    }
}

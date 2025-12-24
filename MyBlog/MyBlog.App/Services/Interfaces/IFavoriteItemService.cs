using MyBlog.App.DTOs.Favorite;

namespace MyBlog.App.Services.Interfaces
{
    public interface IFavoriteItemService
    {
        Task CreateAsync(int blogId, int userId);
        Task RemoveAsync(int itemId);
        Task<FavoriteGetDto> GetFavorite(int userId);
    }
}

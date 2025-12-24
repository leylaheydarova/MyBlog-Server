using MyBlog.App.Models;

namespace MyBlog.App.Repositories.Interfaces
{
    public interface IFavoriteItemRepository
    {
        Task<bool> AddAsync(FavoriteItem item);
        bool Remove(FavoriteItem item);
        IQueryable<FavoriteItem> GetAll(int favoriteID, params string[] includes);
        Task<FavoriteItem> GetByIdAsync(int id, bool isTracking);
        Task<int> SaveAsync();
    }
}

using MyBlog.App.Models;

namespace MyBlog.App.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        bool Remove(Favorite favorite);
        Task<int> SaveAsync();
        Task<Favorite> GetUserFavoriteListAsync(int userId, bool isTracking, params string[] includes);
    }
}

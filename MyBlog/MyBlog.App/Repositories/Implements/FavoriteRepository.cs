using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyBlog.App.Contexts;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;

namespace MyBlog.App.Repositories.Implements
{
    public class FavoriteRepository : IFavoriteRepository
    {
        readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Favorite> GetUserFavoriteListAsync(int userId, bool isTracking, params string[] includes)
        {
            var query = _context.Favorites.AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var favorite = await query.FirstOrDefaultAsync(f => f.UserId == userId);
            return favorite;
        }

        public bool Remove(Favorite favorite)
        {

            EntityEntry entry = _context.Remove(favorite);
            return entry.State == EntityState.Deleted;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

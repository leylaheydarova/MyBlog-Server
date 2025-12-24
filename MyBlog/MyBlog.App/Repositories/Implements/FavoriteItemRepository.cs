using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyBlog.App.Contexts;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;

namespace MyBlog.App.Repositories.Implements
{
    public class FavoriteItemRepository : IFavoriteItemRepository
    {
        readonly AppDbContext _context;

        public FavoriteItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(FavoriteItem item)
        {
            EntityEntry entry = await _context.AddAsync(item);
            return entry.State == EntityState.Added;
        }

        public IQueryable<FavoriteItem> GetAll(int favoriteID, params string[] includes)
        {
            var query = _context.FavoriteItems.Where(i => i.FavoriteId == favoriteID).AsQueryable().AsNoTracking();

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }


        public async Task<FavoriteItem> GetByIdAsync(int id, bool isTracking)
        {
            var query = _context.FavoriteItems.AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(i => i.Id == id);
            return item;

        }

        public bool Remove(FavoriteItem item)
        {

            EntityEntry entry = _context.Remove(item);
            return entry.State == EntityState.Deleted;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

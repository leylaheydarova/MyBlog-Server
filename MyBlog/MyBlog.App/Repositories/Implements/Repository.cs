using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyBlog.App.Contexts;
using MyBlog.App.Models.BaseModels;
using MyBlog.App.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyBlog.App.Repositories.Implements
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry entry = await _context.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public IQueryable<T> GetAll(bool isTracking, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate, bool isTracking, params string[] includes)
        {
            var query = Table.Where(predicate).AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id, bool isTracking, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<T> GetWhereAsync(Expression<Func<T, bool>> predicate, bool isTracking, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.FirstOrDefaultAsync(predicate);
            return entity;
        }

        public bool Remove(T entity)
        {
            EntityEntry entry = _context.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            EntityEntry entry = _context.Update(entity);
            return entry.State == EntityState.Modified;
        }
    }
}

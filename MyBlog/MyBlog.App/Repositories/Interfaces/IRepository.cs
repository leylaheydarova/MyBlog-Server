using Microsoft.EntityFrameworkCore;
using MyBlog.App.Models.BaseModels;
using System.Linq.Expressions;

namespace MyBlog.App.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        Task<bool> AddAsync(T entity);
        bool Remove(T entity);
        bool Update(T entity);
        Task<int> SaveAsync();
        IQueryable<T> GetAll(bool isTracking, params string[] includes);
        Task<T> GetByIdAsync(int id, bool isTracking, params string[] includes);
        Task<T> GetWhereAsync(Expression<Func<T, bool>> predicate, bool isTracking, params string[] includes);
    }
}

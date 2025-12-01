using Microsoft.EntityFrameworkCore;
using MyBlog.App.Models.BaseModels;

namespace MyBlog.App.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        Task<bool> AddAsync(T entity);
        bool Remove(T entity);
        bool Update(T entity);
        IQueryable<T> GetAll(bool isTracking, params string[] includes);
        Task<T> GetById(int id, bool isTracking, params string[] includes);
    }
}

using MyBlog.App.Contexts;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;

namespace MyBlog.App.Repositories.Implements
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}

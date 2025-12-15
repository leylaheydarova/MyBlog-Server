using MyBlog.App.Contexts;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;

namespace MyBlog.App.Repositories.Implements
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context)
        {
        }
    }
}

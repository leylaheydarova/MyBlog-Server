using MyBlog.App.Contexts;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;

namespace MyBlog.App.Repositories.Implements
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }
    }
}

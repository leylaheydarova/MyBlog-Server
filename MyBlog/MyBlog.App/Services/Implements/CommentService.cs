using Microsoft.EntityFrameworkCore;
using MyBlog.App.DTOs.Comment;
using MyBlog.App.Exceptions;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Services.Implements
{
    public class CommentService : ICommentService
    {
        readonly ICommentRepository _commentRepository;
        readonly IBlogRepository _blogRepository;
        readonly IAppUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IBlogRepository blogRepository, IAppUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        public async Task AddComment(CommentCreateDto dto)
        {
            var blog = await _blogRepository.GetByIdAsync(dto.BlogId, false);
            if (blog == null) throw new NotFoundException("blog");

            var user = await _userRepository.GetByIdAsync(dto.UserId, false);
            if (user == null) throw new NotFoundException("user");


            var comment = new Comment()
            {
                CommentText = dto.CommentText,
                UserId = user.Id,
                BlogId = blog.Id,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveAsync();
        }

        public async Task<List<CommentGetDto>> GetAll(int blogId)
        {
            var comments = _commentRepository.GetAllWhere(c => c.BlogId == blogId, false, "User");
            var dtos = new List<CommentGetDto>();
            dtos = await comments.Select(comment => new CommentGetDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                UserName = comment.User.UserName
            }).ToListAsync();

            return dtos;
        }

        public async Task<int> RemoveComment(int commentId, int userId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId, true);
            if (comment.UserId != userId) throw new Exception("You can not remove this comment.");
            var blogId = comment.BlogId;
            _commentRepository.Remove(comment);
            await _commentRepository.SaveAsync();
            return blogId;
        }
    }
}

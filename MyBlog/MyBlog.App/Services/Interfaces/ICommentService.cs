using MyBlog.App.DTOs.Comment;

namespace MyBlog.App.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddComment(CommentCreateDto dto);
        Task<List<CommentGetDto>> GetAll(int blogId);
        Task<int> RemoveComment(int commentId, int userId);
    }
}

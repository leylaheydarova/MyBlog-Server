using MyBlog.App.DTOs.Blog;

namespace MyBlog.App.Services.Interfaces
{
    public interface IBlogService
    {
        Task CreateAsync(BlogCreateDto dto);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, BlogUpdateDto dto);
        Task<List<BlogGetDto>> GetAllAsync();
        Task<BlogGetDto> GetSingleAsync(int id);
    }
}

using MyBlog.App.DTOs.Category;

namespace MyBlog.App.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateDto dto);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, CategoryUpdateDto dto);
        Task<List<CategoryGetDto>> GetAll();
        Task<CategoryGetDto> GetSingleAsync(int id);
    }
}

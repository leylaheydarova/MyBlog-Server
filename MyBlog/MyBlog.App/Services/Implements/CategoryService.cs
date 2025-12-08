using Microsoft.EntityFrameworkCore;
using MyBlog.App.DTOs.Category;
using MyBlog.App.Exceptions;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category()
            {
                Name = dto.Name,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
        }

        public async Task<List<CategoryGetDto>> GetAll()
        {
            var query = _repository.GetAll(false);
            var dtos = new List<CategoryGetDto>();
            dtos = await query.Select(category => new CategoryGetDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate
            }).ToListAsync();
            return dtos;
        }

        public async Task<CategoryGetDto> GetSingleAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id, false);
            if (category == null) throw new NotFoundException("category");
            var dto = new CategoryGetDto()
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate
            };
            return dto;
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id, true);
            if (category == null) throw new NotFoundException("category");

            _repository.Remove(category);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(id, true);
            if (category == null) throw new NotFoundException("category");
            category.Name = dto.Name != null ? dto.Name : category.Name;
            category.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Update(category);
            await _repository.SaveAsync();
        }
    }
}

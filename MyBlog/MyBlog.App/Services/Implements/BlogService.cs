using Microsoft.EntityFrameworkCore;
using MyBlog.App.DTOs.Blog;
using MyBlog.App.Exceptions;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Services.Implements
{
    public class BlogService : IBlogService
    {
        readonly IBlogRepository _repository;

        public BlogService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(BlogCreateDto dto)
        {
            var blog = new Blog()
            {
                Title = dto.Title,
                Text = dto.Text,
                CategoryId = dto.CategoryId,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };
            await _repository.AddAsync(blog);
            await _repository.SaveAsync();
        }

        public async Task<List<BlogGetDto>> GetAllAsync()
        {
            var query = _repository.GetAll(false);
            var dtos = new List<BlogGetDto>();
            dtos = await query.Select(b => new BlogGetDto
            {
                Title = b.Title,
                Text = b.Text,
                CategoryId = b.CategoryId,
                CreatedDate = b.CreatedDate,
                Id = b.Id,
                UpdatedDate = b.UpdatedDate
            }).ToListAsync();
            return dtos;
        }

        public async Task<BlogGetDto> GetSingleAsync(int id)
        {
            var blog = await _repository.GetByIdAsync(id, false);
            if (blog == null) throw new NotFoundException("blog");
            var dto = new BlogGetDto
            {
                Title = blog.Title,
                Text = blog.Text,
                CategoryId = blog.CategoryId,
                CreatedDate = blog.CreatedDate,
                Id = blog.Id,
                UpdatedDate = blog.UpdatedDate
            };
            return dto;
        }

        public async Task RemoveAsync(int id)
        {
            var blog = await _repository.GetByIdAsync(id, true);
            if (blog == null) throw new NotFoundException("blog");

            _repository.Remove(blog);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, BlogUpdateDto dto)
        {
            var blog = await _repository.GetByIdAsync(id, true);
            if (blog == null) throw new NotFoundException("blog");

            blog.Title = dto.Title != null ? dto.Title : blog.Title;
            blog.Text = dto.Text != null ? dto.Text : blog.Text;
            blog.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _repository.Update(blog);
            await _repository.SaveAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MyBlog.App.DTOs.AppUser;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Services.Implements
{
    public class AppUserService : IAppUserService
    {
        readonly IAppUserRepository _repository;

        public AppUserService(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AppUserGetDto>> GetAllAsync()
        {
            var query = _repository.GetAll(false);
            var dtos = new List<AppUserGetDto>();
            dtos = await query.Select(user => new AppUserGetDto
            {
                Id = user.Id,
                CreatedDate = user.CreatedDate,
                Email = user.Email,
                Password = user.Password,
                UpdatedDate = user.UpdatedDate,
                UserName = user.UserName
            }).ToListAsync();
            return dtos;
        }

        public Task<AppUserGetDto> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetWhereAsync(u => u.UserName == dto.UserName && u.Password == dto.Password, false);
            if (user == null) return false;
            return true;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = new AppUser()
            {
                Email = dto.Email,
                Password = dto.Password,
                UserName = dto.UserName,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };

            await _repository.AddAsync(user);
            await _repository.SaveAsync();
        }

        public Task RemoveAccountAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, AppUserUpdateDto dto)
        {
            throw new NotImplementedException();
        }


    }
}

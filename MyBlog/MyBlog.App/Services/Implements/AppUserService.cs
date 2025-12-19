using Microsoft.EntityFrameworkCore;
using MyBlog.App.DTOs.AppUser;
using MyBlog.App.Exceptions;
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

        public async Task<AppUserGetDto> GetSingleAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id, false);
            if (user == null) throw new NotFoundException("user");
            return new AppUserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate,
                UserName = user.UserName
            };
        }

        public async Task<int> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetWhereAsync(u => u.UserName == dto.UserName && u.Password == dto.Password, false);
            if (user == null) throw new Exception("Email or password incorrect");
            return user.Id;
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

        public async Task RemoveAccountAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id, true);
            if (user == null) throw new NotFoundException("user");
            _repository.Remove(user);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, AppUserUpdateDto dto)
        {
            var user = await _repository.GetByIdAsync(id, true);
            if (user == null) throw new NotFoundException("user");
            user.Email = dto.Email != null ? dto.Email : user.Email;
            user.UserName = dto.Username != null ? dto.Username : user.UserName;
            user.Password = dto.Password != null ? dto.Password : user.Password;
            user.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Update(user);
            await _repository.SaveAsync();
        }
    }
}

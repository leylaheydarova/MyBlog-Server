using MyBlog.App.DTOs.AppUser;

namespace MyBlog.App.Services.Interfaces
{
    public interface IAppUserService
    {
        Task RegisterAsync(RegisterDto dto);
        Task RemoveAccountAsync(int id);
        Task UpdateAsync(int id, AppUserUpdateDto dto);
        Task<List<AppUserGetDto>> GetAllAsync();
        Task<AppUserGetDto> GetSingleAsync(int id);
        Task<int> LoginAsync(LoginDto dto);
    }
}

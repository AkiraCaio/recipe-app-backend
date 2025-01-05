using RecipeAppBackend.Application.DTO;

namespace RecipeAppBackend.Application.Interface
{
    public interface IUserService
    { 
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<UserDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
       
        Task<UserDto> AddAsync(UserCreateDto userCreateDto); 
        Task UpdateAsync(UserDto userDto);
        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
using RecipeAppBackend.Domain.Entities;

namespace RecipeAppBackend.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();

        Task addAsync(User user);
        Task updateAsync(User user);
        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
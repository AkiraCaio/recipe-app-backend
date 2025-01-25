using RecipeAppBackend.Domain.Entities;

namespace RecipeAppBackend.Domain.Interfaces
{
    public interface IRoleRepository 
    {
        Task<Role> GetByIdAsync(int id);
        Task<Role> GetByNameAsync(string name);
        Task<IEnumerable<Role>> GetAllAsync();

        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
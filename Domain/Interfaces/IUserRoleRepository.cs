using RecipeAppBackend.Domain.Entities;

namespace RecipeAppBackend.Domain.Interfaces { 
    public interface IUserRoleRepository {
        Task<UserRole> GetByIdAsync(int id);
        Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetAllAsync();

        Task AddAsync(UserRole userRole);
        Task UpdateAsync(UserRole userRole);
        Task DeleteAsync(int id);
    }
}
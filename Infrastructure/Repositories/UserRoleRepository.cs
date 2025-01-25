using Microsoft.EntityFrameworkCore;
using RecipeAppBackend.Domain.Entities;
using RecipeAppBackend.Domain.Interfaces;
using RecipeAppBackend.Infrastructure.Data;

namespace RecipeAppBackend.Infrastructure.Repositories 
{ 
    public class UserRoleRepository : IUserRoleRepository 
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<UserRole> GetByIdAsync(int id) 
        {
            return await _context.UserRoles.FindAsync(id);
        }

        public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync() 
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task AddAsync(UserRole userRole) 
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserRole userRole) 
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null) 
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
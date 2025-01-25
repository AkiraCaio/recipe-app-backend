using Microsoft.EntityFrameworkCore;
using RecipeAppBackend.Domain.Interfaces;
using RecipeAppBackend.Domain.Entities;
using RecipeAppBackend.Infrastructure.Data;

namespace RecipeAppBackend.Infrastructure.Repositories
{
    public class RoleRepository(AppDbContext context) : IRoleRepository {
        private readonly AppDbContext _context = context;

        public async Task<Role> GetByIdAsync(int id) {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> GetByNameAsync(string name) {
            return await _context.Roles.SingleOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Role>> GetAllAsync() {
            return await _context.Roles.ToListAsync();
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Roles.AnyAsync(r => r.RoleId == id);
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            return _context.Roles.AnyAsync(r => r.Name == name);
        }
    }
}
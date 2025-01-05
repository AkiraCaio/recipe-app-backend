using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipeAppBackend.Infrastructure.Data;

namespace RecipeAppBackend.Extensions
{
    public static class DbExtensions
    {
        public static IServiceCollection AddDbContexts(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(defaultConnection));

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RecipeAppBackend.Domain.Entities;

namespace RecipeAppBackend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {        
        }

        public DbSet<User> Users { get; set; }
        // public DbSet<Recipe> Recipes { get; set; }

         // Se precisar ajustar mapeamentos finos, utilize o OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemplo de como mapear nomes de tabelas ou relacionamentos
            modelBuilder.Entity<User>().ToTable("Users");
            // modelBuilder.Entity<Recipe>().ToTable("Recipes");

            // Mapeamento de relacionamentos, índices, chaves compostas etc.
            // modelBuilder.Entity<Recipe>()
            //     .HasOne(r => r.User)
            //     .WithMany(u => u.Recipes)
            //     .HasForeignKey(r => r.UserId);

            // Outras configurações específicas...
        }
    }
}
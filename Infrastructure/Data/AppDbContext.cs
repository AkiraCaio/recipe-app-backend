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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MARK: Users
            modelBuilder.Entity<User>(entity => { 
                // Primary Key
                entity.HasKey(u => u.UserId);

                // Table Name
                entity.ToTable("User");

                // Properties
                entity.HasIndex(u => u.Username)
                    .IsUnique();
                
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                    .IsRequired();
                
                entity.Property(u => u.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(u => u.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(u => u.IsActive)
                    .HasColumnName("is_active")
                    .IsRequired();
                
                entity.Property(u => u.LastLoginAt)
                    .HasColumnName("last_login_at")
                    .HasColumnType("datetime");
            });

            // MARK: Role
            modelBuilder.Entity<Role>(entity =>
            {
                // Primary Key
                entity.HasKey(r => r.RoleId);

                // Table Name
                entity.ToTable("Role");

                // Properties
                entity.Property(r => r.Name).IsRequired();

                entity.Property(r => r.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(r => r.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(r => r.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            // MARK: UserRole
            modelBuilder.Entity<UserRole>(entity =>
            {
                // Primary Key
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                // Table Name
                entity.ToTable("UserRoles");

                // Relacionamento com User
                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                // Relacionamento com Role
                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });
        }
    }
}
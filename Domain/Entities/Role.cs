namespace RecipeAppBackend.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        // MARK: Auditoria / estado da conta
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // MARK: Navegações
        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
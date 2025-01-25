namespace RecipeAppBackend.Domain.Entities
{ 
    public class UserRole 
    { 
        // Chaves compostas
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
        // MARK: Auditoria / estado da conta
        public int AssignedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Reason { get; set; }
        public DateTime? ExpiringAt { get; set; }

        // MARK: Navegações
        public User User { get; set; }
        public Role Role { get; set; } 

        // MARK: Construtores
        public UserRole(int userId, int roleId) {
            UserId = userId;
            RoleId = roleId;
            CreatedAt = DateTime.UtcNow;
            AssignedBy = -1; // Valor padrão para indicar que não foi atribuído por um usuário
         }
    }
}
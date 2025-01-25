namespace RecipeAppBackend.Domain.Entities
{
    public class User 
    { 
        public int UserId { get; set; }

        // MARK: Informações de identificação
        public required string Username { get; set; } // valor unico
        public required string Email { get; set; } // valor unico
        public required string PasswordHash { get; set; }

        // MARK: Auditoria / estado da conta
        public DateTime CreatedAt { get; set; }        // Quando a conta foi criada
        public DateTime? UpdatedAt { get; set; }       // Última vez que dados de User foram alterados
        public bool IsActive { get; set; }             // Conta ativa ou suspensa?
        public DateTime? LastLoginAt { get; set; }     // Data/hora do último login bem-sucedido
        
        // MARK: Navegações 
        public ICollection<UserRole> UserRoles { get; set; } // Propriedade de navegação para o relacionamento N:N via UserRole

        public User()
        {
            // Valores padrão:
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
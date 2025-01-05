namespace RecipeAppBackend.Domain.Entities
{
    public class User 
    { 
        public int Id { get; set; }

        // MARK: Informações de identificação
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; } 

        // MARK: Auditoria / estado da conta
        public DateTime CreatedAt { get; set; }        // Quando a conta foi criada
        public DateTime? UpdatedAt { get; set; }       // Última vez que dados de User foram alterados
        public bool IsActive { get; set; }             // Conta ativa ou suspensa?
        public DateTime? LastLoginAt { get; set; }     // Data/hora do último login bem-sucedido

        // Construtor simples (opcional)
        public User()
        {
            // Valores padrão:
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
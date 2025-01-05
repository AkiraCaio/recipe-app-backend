using RecipeAppBackend.Application.DTO;

namespace RecipeAppBackend.Application.Interface
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto loginDto);
    }
}

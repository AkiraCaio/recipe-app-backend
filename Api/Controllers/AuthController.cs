using Microsoft.AspNetCore.Mvc;
using RecipeAppBackend.Application.DTO;
using RecipeAppBackend.Application.Interface;

namespace RecipeAppBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(token);
        }
    }
}
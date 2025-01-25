using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeAppBackend.Application.Interface;
using RecipeAppBackend.Application.DTO;
using System.Security.Claims;
using RecipeAppBackend.Application.Exceptions;

namespace RecipeAppBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Exige um token JWT válido para acessar estes endpoints
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retorna um usuário específico pelo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) { 
                return Unauthorized();
            } 
            
            var authenticateUserId = int.Parse(userId);
            if (authenticateUserId != id) { 
                return Forbid();
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // Permite acesso sem token JWT
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            try
            {
                var createdUser = await _userService.AddAsync(userCreateDto);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = createdUser.UserId }, createdUser);            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Retorna as permissões do usuário
        /// </summary>
        [HttpGet("permissions")]
        public async Task<IActionResult> GetUserPermissionsAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) { 
                return Unauthorized();
            } 
    
            var authenticateUserId = int.Parse(userId);

            var permissions = await _userService.GetUserPermissionsAsync(authenticateUserId);
            if (permissions == null || !permissions.Any())
            {
                return NotFound("Permissions not found.");
            }

            return Ok(permissions);
        }        
    }
}
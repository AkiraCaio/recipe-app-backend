using AutoMapper;
using RecipeAppBackend.Application.DTO;
using RecipeAppBackend.Application.Interface;
using RecipeAppBackend.Application.Exceptions;
using RecipeAppBackend.Domain.Entities;
using RecipeAppBackend.Domain.Interfaces;


namespace RecipeAppBackend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> AddAsync(UserCreateDto userCreateDto)
        {
            if (await _userRepository.ExistsByEmailAsync(userCreateDto.Email))
            {
                throw new ConflictException("Email already exists.");
            }

            if (await _userRepository.ExistsByUsernameAsync(userCreateDto.Username))
            {
                throw new ConflictException("Username already exists.");
            }

            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

            var userRole = await _roleRepository.GetByNameAsync("User");
            if (userRole == null)
            {
                throw new Exception("Role 'User' not found.");
            }

            await _userRepository.AddAsync(user);
            var userRoleAssignment = new UserRole(user.UserId, userRole.RoleId) { Reason = "create user" };
            await _userRoleRepository.AddAsync(userRoleAssignment);

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _userRepository.ExistsAsync(id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _userRepository.ExistsByEmailAsync(email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _userRepository.ExistsByUsernameAsync(username);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<string>> GetUserPermissionsAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var permissions = new List<string>();

            foreach (var userRole in user.UserRoles)
            {
              permissions.AddRange(userRole.Role.Name);
            }

            return permissions.Distinct();
        }
    }
}
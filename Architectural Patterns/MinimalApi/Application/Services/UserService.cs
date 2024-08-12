using Application.Services.Interfaces;
using Domain.DTO;
using Domain.Model;
using Infrastructure.Repository.Interface;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, IGenericRepository<User> userRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentNullException(nameof(email));
                }

                var user = await _userRepository.FirstOrDefaultAsync(x => x.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }

                return new UserDTO()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the user by email", ex);
            }
        }

        public async Task<bool> VerifyPasswordAsync(UserDTO user, string password)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(user);

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }

                return await _userRepository.AnyAsync(x =>
                    x.Email.Contains(user.Email, StringComparison.OrdinalIgnoreCase) &&
                    x.Password == password);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while verifying the password", ex);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return users.Select(x => new UserDTO()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Email = x.Email
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all users", ex);
            }
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }

                return new UserDTO()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the user by ID", ex);
            }
        }

        public async Task AddAsync(UserAddDTO userDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(userDto);

                var user = new User()
                {
                    Email = userDto.Email,
                    Id = userDto.Id,
                    Name = userDto.Name,
                    Password = userDto.Password
                };

                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the user", ex);
            }
        }

        public async Task UpdateAsync(UserDTO userDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(userDto);

                var user = new User()
                {
                    Email = userDto.Email,
                    Id = userDto.Id,
                    Name = userDto.Name
                };

                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the user", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }

                await _userRepository.RemoveAsync(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the user", ex);
            }
        }
    }
}

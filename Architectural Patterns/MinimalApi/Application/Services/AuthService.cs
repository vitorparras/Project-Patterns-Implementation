using Application.Services.Interfaces;
using Domain.DTO;
using Domain.Model;
using Infrastructure.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<TokenHistory> _tokenHistoryRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, IGenericRepository<TokenHistory> tokenHistoryRepository, IUserService userService)
        {
            _configuration = configuration;
            _tokenHistoryRepository = tokenHistoryRepository;
            _userService = userService;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentNullException(nameof(email));
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }

                var user = await _userService.GetByEmailAsync(email);
                if (user == null || !await _userService.VerifyPasswordAsync(user, password))
                {
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }

                var token = await GenerateJwtToken(user);
                await SaveTokenToHistoryAsync(user.Id, token);
                return token;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while logging in", ex);
            }
        }

        public async Task LogoutAsync(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new ArgumentNullException(nameof(token));
                }

                var tokenHistory = await _tokenHistoryRepository.FirstOrDefaultAsync(x =>
                        x.Token.Contains(token, StringComparison.OrdinalIgnoreCase)) ?? throw new KeyNotFoundException("Token Not Found.");

                if (tokenHistory.IsValid)
                {
                    tokenHistory.IsValid = false;
                    await _tokenHistoryRepository.UpdateAsync(tokenHistory);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while logging out", ex);
            }
        }

        public async Task<bool> TokenIsValid(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new ArgumentNullException(nameof(token));
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var tokenValid = await _tokenHistoryRepository.AnyAsync(x => x.IsValid && x.Token.Contains(token, StringComparison.OrdinalIgnoreCase));

                return validatedToken != null && tokenValid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<string> GenerateJwtToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            await SaveTokenToHistoryAsync(user.Id, stringToken);

            return stringToken;
        }

        private async Task SaveTokenToHistoryAsync(Guid userId, string token)
        {
            var tokenHistory = new TokenHistory
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                IsValid = true
            };

            await _tokenHistoryRepository.AddAsync(tokenHistory);
        }
    }
}


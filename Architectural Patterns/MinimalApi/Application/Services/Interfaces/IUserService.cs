using Domain.DTO;
using Domain.Model;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetByEmailAsync(string email);
        Task<bool> VerifyPasswordAsync(UserDTO user, string password);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(Guid id);
        Task AddAsync(UserAddDTO user);
        Task UpdateAsync(UserDTO user);
        Task DeleteAsync(Guid id);
    }
}

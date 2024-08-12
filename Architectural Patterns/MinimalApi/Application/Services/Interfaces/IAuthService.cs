namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task LogoutAsync(string token);
        Task<bool> TokenIsValid(string token);
    }
}

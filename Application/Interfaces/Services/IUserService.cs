using StockApp.Core.Application.Dtos.Account;
using StockApp.Core.Application.ViewModels.Users;

namespace StockApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasssowrdAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasssowrdAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
    }
}
using Microsoft.AspNetCore.Identity;
using SchoolAdministrator.Data.Entities;
using SchoolAdministrator.Models;

namespace SchoolAdministrator.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<User> AddUserAsync(AddUserViewModel model, Guid imageId);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<User> GetUserAsync(Guid userId);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);




    }
}

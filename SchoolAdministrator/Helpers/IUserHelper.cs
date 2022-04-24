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

    }
}

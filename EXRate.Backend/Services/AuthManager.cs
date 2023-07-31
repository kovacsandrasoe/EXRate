using EXRate.Backend.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EXRate.Backend.Services
{
    public class AuthManager : IAuthManager
    {
        UserManager<AppUser> userManager;

        public AuthManager(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GetCurrentUserId(ClaimsPrincipal claimsPrincipal)
        {
            var result = await userManager.GetUserAsync(claimsPrincipal);
            if (result == null)
            {
                throw new Exception("user not found");
            }
            else
            {
                return result.Id;
            }
        }
    }
}

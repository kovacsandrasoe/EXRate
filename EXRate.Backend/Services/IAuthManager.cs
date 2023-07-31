using System.Security.Claims;

namespace EXRate.Backend.Services
{
    public interface IAuthManager
    {
        Task<string> GetCurrentUserId(ClaimsPrincipal claimsPrincipal);
    }
}
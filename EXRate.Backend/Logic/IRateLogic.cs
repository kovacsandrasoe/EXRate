using EXRate.Backend.Models;
using EXRate.Backend.ViewModels;
using System.Security.Claims;

namespace EXRate.Backend.Logic
{
    public interface IRateLogic
    {
        Task Add(RateRecordCreateModel r, ClaimsPrincipal user);
        Task Delete(string id, ClaimsPrincipal user);
        Task<IEnumerable<RateRecord>> Get(ClaimsPrincipal user);
        Task Update(RateRecord r, ClaimsPrincipal user);
    }
}
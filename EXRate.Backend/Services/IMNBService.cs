using EXRate.Backend.Models;

namespace EXRate.Backend.Services
{
    public interface IMNBService
    {
        Task<IEnumerable<Rate>> GetRates();
    }
}
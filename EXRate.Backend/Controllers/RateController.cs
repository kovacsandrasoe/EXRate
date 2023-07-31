using EXRate.Backend.Models;
using EXRate.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXRate.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RateController : ControllerBase
    {
        IMNBService service;

        public RateController(IMNBService service)
        {
            this.service = service;
        }

        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "none")] //óránként elég az MNB-t nyaggatni sztem.
        public async Task<IEnumerable<Rate>> Get()
        {
            return await this.service.GetRates();
        }
    }
}

using EXRate.Backend.Models;
using EXRate.Backend.Repository;
using EXRate.Backend.Services;
using EXRate.Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXRate.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RateRecordController : ControllerBase
    {
        IRateRecordRepository repo;
        IMNBService mnb;
        IAuthManager auth;

        public RateRecordController(IRateRecordRepository repo, IMNBService mnb, IAuthManager auth)
        {
            this.repo = repo;
            this.mnb = mnb;
            this.auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> Add(RateRecordCreateModel r)
        {
            try
            {
                RateRecord record = new RateRecord();
                record.Comment = r.Comment;
                record.Currency = r.Currency;
                record.Value = (await mnb.GetRates()).FirstOrDefault(z => z.Current == r.Currency).Value;
                record.TimeAdded = DateTime.Now;
                record.Creator = await this.auth.GetCurrentUserId(this.User);
                await repo.AddRateRecordAsync(record);
            }
            catch(Exception e)
            {
                return BadRequest(new Errorinfo(e.Message));
            }
            return Ok();   
        }

        [HttpGet]
        public async Task<IEnumerable<RateRecord>> Get()
        {
            var userId = await this.auth.GetCurrentUserId(this.User);
            return (await repo.GetRecords()).Where(t => t.Creator == userId);
        }


    }
}

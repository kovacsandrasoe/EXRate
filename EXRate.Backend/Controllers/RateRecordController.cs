using EXRate.Backend.Models;
using EXRate.Backend.Repository;
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

        public RateRecordController(IRateRecordRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult> Add(RateRecordCreateModel r)
        {
            try
            {
                RateRecord record = new RateRecord();
                record.Comment = r.Comment;
                record.Currency = r.Currency;
                record.Value = r.Value;
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
            return await repo.GetRecords();
        }


    }
}

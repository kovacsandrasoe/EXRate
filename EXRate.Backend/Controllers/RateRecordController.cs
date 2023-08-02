using EXRate.Backend.Logic;
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
        IRateLogic logic;

        public RateRecordController(IRateLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public async Task<ActionResult> Add(RateRecordCreateModel r)
        {
            try
            {
                await logic.Add(r, this.User);
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
            return await logic.Get(this.User);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await logic.Delete(id, this.User);
            }
            catch (Exception ex)
            {
                return BadRequest(new Errorinfo(ex.Message));
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(RateRecord r)
        {
            try
            {
                await logic.Update(r, this.User);
            }
            catch (Exception ex)
            {
                return BadRequest(new Errorinfo(ex.Message));
            }
            return Ok();
        }


    }
}

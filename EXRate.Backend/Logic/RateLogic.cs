using EXRate.Backend.Models;
using EXRate.Backend.Repository;
using EXRate.Backend.Services;
using EXRate.Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EXRate.Backend.Logic
{
    public class RateLogic : IRateLogic
    {
        IRateRecordRepository repo;
        IMNBService mnb;
        IAuthManager auth;

        public RateLogic(IRateRecordRepository repo, IMNBService mnb, IAuthManager auth)
        {
            this.repo = repo;
            this.mnb = mnb;
            this.auth = auth;
        }

        public async Task Add(RateRecordCreateModel r, ClaimsPrincipal user)
        {
            //todo: Automapperrel kéne, de a Claim kinyerés miatt ronda lenne ígyis úgyis
            RateRecord record = new RateRecord();
            record.Comment = r.Comment;
            record.Currency = r.Currency;
            record.Value = (await mnb.GetRates()).FirstOrDefault(z => z.Currency == r.Currency).Value;
            record.TimeAdded = DateTime.Now;
            record.Creator = await this.auth.GetCurrentUserId(user);
            await repo.AddRateRecordAsync(record);
        }

        public async Task<IEnumerable<RateRecord>> Get(ClaimsPrincipal user)
        {
            var userId = await this.auth.GetCurrentUserId(user);
            return (await repo.GetRecordsAsync()).Where(t => t.Creator == userId);
        }

        public async Task Delete(string id, ClaimsPrincipal user)
        {
            var userId = await this.auth.GetCurrentUserId(user);
            var record = (await this.repo.GetRecordsAsync()).FirstOrDefault(t => t.Id == id);
            if (userId == record.Creator)
            {
                await this.repo.DeleteRateRecordAsync(id);
            }
        }

        [HttpPut]
        public async Task Update(RateRecord r, ClaimsPrincipal user)
        {
            var userId = await this.auth.GetCurrentUserId(user);
            var record = (await this.repo.GetRecordsAsync()).FirstOrDefault(t => t.Id == r.Id);
            if (userId == record?.Creator)
            {
                await this.repo.UpdateRecordAsync(r);
            }
        }
    }
}

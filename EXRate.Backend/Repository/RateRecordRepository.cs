using EXRate.Backend.Data;
using EXRate.Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EXRate.Backend.Repository
{
    public class RateRecordRepository : IRateRecordRepository
    {
        EXRateContext ctx;

        public RateRecordRepository(EXRateContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddRateRecordAsync(RateRecord r)
        {
            await this.ctx.Records.AddAsync(r);
            await this.ctx.SaveChangesAsync();
        }

        public async Task DeleteRateRecordAsync(string id)
        {
            this.ctx.Records.Remove(await FindRecordById(id));
            await this.ctx.SaveChangesAsync();
        }

        private async Task<RateRecord> FindRecordById(string id)
        {
            return await this.ctx.Records.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<RateRecord>> GetRecordsAsync()
        {
            return this.ctx.Records;
        }

        public async Task UpdateRecordAsync(RateRecord r)
        {
            var old = await FindRecordById(r.Id);
            old.Comment = r.Comment;
            await this.ctx.SaveChangesAsync();
        }
    }
}

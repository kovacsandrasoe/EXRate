﻿using EXRate.Backend.Models;

namespace EXRate.Backend.Repository
{
    public interface IRateRecordRepository
    {
        Task AddRateRecordAsync(RateRecord r);
        Task DeleteRateRecordAsync(string id);
        Task<IEnumerable<RateRecord>> GetRecordsAsync();
        Task UpdateRecordAsync(RateRecord r);
    }
}
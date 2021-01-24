using Linky.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linky.DataAccessLayer.Repositories.Abstract
{
    public interface IDailyCounterRepository
    {
        Task<DailyCounter> GetDailyCounterAsync(int id);
        Task<DailyCounter> GetDailyCounterAsync(int id, DateTime day);

        Task<IEnumerable<DailyCounter>> GetDailyCountersAsync();
        Task<IEnumerable<DailyCounter>> GetDailyCountersAsync(DateTime firstDay, DateTime lastDay);
        Task<IEnumerable<DailyCounter>> GetDailyCountersAsync(int linkId, DateTime firstDay, DateTime lastDay);

        Task<bool> SaveDailyCounterAsync(DailyCounter dailyCounter);
        Task<bool> DeleteDailyCounterAsync(DailyCounter dailyCounter);
    }
}

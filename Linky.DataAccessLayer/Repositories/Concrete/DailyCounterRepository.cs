using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Linky.DataAccessLayer.Repositories.Concrete
{
    public class DailyCounterRepository : BaseRepository, IDailyCounterRepository
    {
        public async Task<DailyCounter> GetDailyCounterAsync(int id)
        {
            return await context.DailyCounters.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DailyCounter> GetDailyCounterAsync(int linkId, DateTime day)
        {
            return await context.DailyCounters
                .Where(c => c.LinkId == linkId)
                .FirstOrDefaultAsync(x => x.Day == day.Date);
        }

        public async Task<IEnumerable<DailyCounter>> GetDailyCountersAsync()
        {
            return await context.DailyCounters.ToListAsync();
        }

        public async Task<IEnumerable<DailyCounter>> GetDailyCountersAsync(DateTime firstDay, DateTime lastDay)
        {
            return await context.DailyCounters
                .Where(c => DbFunctions.TruncateTime(c.Day) >= firstDay.Date && DbFunctions.TruncateTime(c.Day) < lastDay.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<DailyCounter>> GetDailyCountersAsync(int linkId, DateTime firstDay, DateTime lastDay)
        {
            return await context.DailyCounters
                .Where(c => c.LinkId == linkId)
                .Where(c => DbFunctions.TruncateTime(c.Day) >= firstDay.Date && DbFunctions.TruncateTime(c.Day) < lastDay.Date)
                .ToListAsync();
        }

        public async Task<bool> SaveDailyCounterAsync(DailyCounter dailyCounter)
        {
            if (dailyCounter == null)
            {
                return false;
            }

            try
            {
                context.Entry(dailyCounter).State = dailyCounter.Id == default ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteDailyCounterAsync(DailyCounter dailyCounter)
        {
            if (dailyCounter == null)
                return false;

            context.DailyCounters.Remove(dailyCounter);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}

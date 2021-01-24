using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Linky.DataAccessLayer.Repositories.Concrete
{
    public class CountryCounterRepository : BaseRepository, ICountryCounterRepository
    {
        public async Task<CountryCounter> GetCountryCounterAsync(int id)
        {
            return await context.CountryCounters.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CountryCounter>> GetCountryCountersAsync()
        {
            return await context.CountryCounters.ToListAsync();
        }

        public async Task<bool> SaveCountryCounterAsync(CountryCounter countryCounter)
        {
            if (countryCounter == null)
            {
                return false;
            }

            try
            {
                context.Entry(countryCounter).State = countryCounter.Id == default ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteCountryCounterAsync(CountryCounter countryCounter)
        {
            if (countryCounter == null)
                return false;

            context.CountryCounters.Remove(countryCounter);

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

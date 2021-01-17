using Linky.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linky.DataAccessLayer.Repositories.Abstract
{
    public interface ICountryCounterRepository
    {
        Task<CountryCounter> GetCountryCounterAsync(int id);
        Task<CountryCounter> GetCountryCounterAsync(string countryName);

        Task<IEnumerable<CountryCounter>> GetCountryCountersAsync();

        Task<bool> SaveCountryCounterAsync(CountryCounter countryCounter);
        Task<bool> DeleteCountryCounterAsync(CountryCounter countryCounter);
    }
}

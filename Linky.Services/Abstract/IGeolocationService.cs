using System.Threading.Tasks;

namespace Linky.Services.Abstract
{
    public interface IGeolocationService
    {
        Task<string> GetCountryName(string ipAddress);
    }
}

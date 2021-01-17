using Linky.Services.Abstract;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Linky.Services.Concrete
{
    public class GeolocationService : IGeolocationService
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string URL = @"http://ip-api.com/line/";

        public async Task<string> GetCountryName(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentException("IP Address is empty or null", nameof(ipAddress));
            }

            if (ipAddress.Equals("127.0.0.1") || ipAddress.Equals("localhost"))
            {
                // Placeholder
                ipAddress = "194.181.92.98";
            }

            return await _client.GetStringAsync(URL + ipAddress + "?fields=country");
        }
    }
}

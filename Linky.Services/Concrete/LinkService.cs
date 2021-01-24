using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using Linky.Services.Abstract;
using Linky.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linky.Services.Concrete
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IGeolocationService _geolocationService;

        public LinkService(ILinkRepository linkRepository, IGeolocationService geolocationService)
        {
            _linkRepository = linkRepository;
            _geolocationService = geolocationService;
        }

        public async Task<Link> FindByIdAsync(int id)
        {
            return await _linkRepository.GetLinkAsync(id);
        }

        public async Task<Link> FindByLabelAsync(string label)
        {
            return await _linkRepository.GetLinkAsync(label);
        }

        public async Task<IEnumerable<Link>> GetUserLinksAsync(string userId)
        {
            return await _linkRepository.GetLinksAsync(userId);
        }

        public IEnumerable<DailyCounter> GetDailyStatistics(Link link, int days)
        {
            var today = DateTime.Now.Date;
            var past = today.AddDays(-days);

            var dailyCounters = link.DailyCounters.Where(x => x.Day.Date >= past && x.Day.Date <= today).ToList();

            // Make the daily range consistent
            //var range = new List<DailyCounter>()
            //{
            //    new DailyCounter() { Day = today },
            //    new DailyCounter() { Day = past }
            //};

            //dailyCounters.AddRange(range.Where(r => !dailyCounters.Any(c => c.Day.Date == r.Day.Date)));
            return dailyCounters;
        }

        public async Task<LinkServiceResponse> AddLinkAsync(Link link, string userId)
        {
            var response = new LinkServiceResponse();

            var repoLink = await _linkRepository.GetLinkAsync(link.Label);
            if (repoLink != null)
            {
                response.ErrorMessage = "A link with the specified label already exists";
                return response;
            }

            link.ApplicationUserId = userId;

            var result = await _linkRepository.SaveLinkAsync(link);

            if (!result)
            {
                response.ErrorMessage = "Could not save the link to the database";
                return response;
            }

            response.Success = true;
            return response;
        }

        public async Task<LinkServiceResponse> EditLinkAsync(Link link)
        {
            var response = new LinkServiceResponse();

            var repoLink = await _linkRepository.GetLinkAsync(link.Label);
            if (repoLink != null)
            {
                response.ErrorMessage = "A link with the specified label already exists";
                return response;
            }

            var result = await _linkRepository.SaveLinkAsync(link);

            if (!result)
            {
                response.ErrorMessage = "Could not save the link to the database";
                return response;
            }

            response.Success = true;
            return response;
        }

        public async Task<LinkServiceResponse> DeleteLinkAsync(Link link)
        {
            var response = new LinkServiceResponse();
            var result = await _linkRepository.DeleteLinkAsync(link);

            if(!result)
            {
                response.ErrorMessage = "Could not delete the link from the database";
                return response;
            }

            response.Success = true;
            return response;
        }

        public async Task<LinkServiceRedirectResponse> HandleClickAsync(string label, string ipAddress)
        {
            var response = new LinkServiceRedirectResponse();
            var link = await _linkRepository.GetLinkAsync(label);

            if (link == null)
            {
                response.ErrorMessage = "A link with the specified label does not exist";
                return response;
            }

            UpdateClickStatistics(link);
            await UpdateCountryStatisticsAsync(link, ipAddress);
            UpdateDailyStatistics(link);

            var success = await _linkRepository.SaveLinkAsync(link);
            
            if (!success)
            {
                response.ErrorMessage = "Server error while saving changes to the database";
                return response;
            }

            response.Success = true;
            response.URL = link.URL;
            return response;
        }

        private void UpdateClickStatistics(Link link)
        {
            link.Clicks++;
        }

        private async Task<bool> UpdateCountryStatisticsAsync(Link link, string ipAddress)
        {
            string countryName;

            try
            {
                countryName = await _geolocationService.GetCountryName(ipAddress);
            }
            catch (Exception)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(countryName))
            {
                return false;
            }

            var countryCounter = link.CountryCounters.FirstOrDefault(x => x.CountryName == countryName);

            if (countryCounter == null)
            {
                countryCounter = new CountryCounter
                {
                    CountryName = countryName,
                    LinkId = link.Id,
                    Clicks = 0
                };

                link.CountryCounters.Add(countryCounter);
            }

            countryCounter.Clicks++;
            return true;
        }

        private void UpdateDailyStatistics(Link link)
        {
            var today = DateTime.Now.Date;
            var dailyCounter = link.DailyCounters.FirstOrDefault(x => x.Day.Date == today);

            if (dailyCounter == null)
            {
                dailyCounter = new DailyCounter
                {
                    LinkId = link.Id,
                    Clicks = 0,
                    Day = today
                };

                link.DailyCounters.Add(dailyCounter);
            }

            dailyCounter.Clicks++;
        }
    }
}

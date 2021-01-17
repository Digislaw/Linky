using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using Linky.Services.Abstract;
using Linky.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linky.Services.Concrete
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly ICountryCounterRepository _countryCounterRepository;
        private readonly IGeolocationService _geolocationService;

        public LinkService(ILinkRepository linkRepository, 
            ICountryCounterRepository countryCounterRepository, 
            IGeolocationService geolocationService)
        {
            _linkRepository = linkRepository;
            _countryCounterRepository = countryCounterRepository;
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

            var result = await UpdateClickStatistics(link, ipAddress);

            if (!result)
            {
                response.ErrorMessage = "Server error while saving changes to the database";
                return response;
            }

            response.Success = true;
            response.URL = link.URL;
            return response;
        }

        private async Task<bool> UpdateClickStatistics(Link link, string ipAddress)
        {
            link.Clicks++;

            var linkSaveResult = await _linkRepository.SaveLinkAsync(link);

            if (!linkSaveResult)
            {
                return false;
            }

            string countryName;

            try
            {
                countryName = await _geolocationService.GetCountryName(ipAddress);
            }
            catch (System.Exception)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(countryName))
            {
                return false;
            }

            var countryCounter = await _countryCounterRepository.GetCountryCounterAsync(countryName);

            if (countryCounter == null)
            {
                countryCounter = new CountryCounter
                {
                    CountryName = countryName,
                    LinkId = link.Id,
                    Clicks = 0
                };
            }

            countryCounter.Clicks++;
            return await _countryCounterRepository.SaveCountryCounterAsync(countryCounter);
        }
    }
}

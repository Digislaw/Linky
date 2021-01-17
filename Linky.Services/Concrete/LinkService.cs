using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using Linky.Services.Abstract;
using Linky.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linky.Services.Concrete
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public async Task<Link> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
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

        public async Task<LinkServiceRedirectResponse> HandleClickAsync(string label)
        {
            var response = new LinkServiceRedirectResponse();
            var link = await _linkRepository.GetLinkAsync(label);

            if (link == null)
            {
                response.ErrorMessage = "A link with the specified label does not exist";
                return response;
            }

            link.Clicks++;
            var result = await _linkRepository.SaveLinkAsync(link);

            if (!result)
            {
                response.ErrorMessage = "Server error while saving changes to the database";
                return response;
            }

            response.Success = true;
            response.URL = link.URL;
            return response;
        }
    }
}

using Linky.Entities.Models;
using Linky.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linky.Services.Abstract
{
    public interface ILinkService
    {
        Task<Link> FindByIdAsync(int id);
        Task<Link> FindByLabelAsync(string label);

        Task<IEnumerable<Link>> GetUserLinksAsync(string userId);

        Task<LinkServiceResponse> AddLinkAsync(Link link, string userId);
        Task<LinkServiceResponse> EditLinkAsync(Link link);
        Task<LinkServiceResponse> DeleteLinkAsync(Link link);

        Task<LinkServiceRedirectResponse> HandleClickAsync(string label, string ipAddress);
    }
}

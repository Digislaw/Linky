using Linky.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linky.DataAccessLayer.Repositories.Abstract
{
    public interface ILinkRepository
    {
        Task<Link> GetLinkAsync(int id);
        Task<Link> GetLinkAsync(string label);

        Task<IEnumerable<Link>> GetLinksAsync();
        Task<IEnumerable<Link>> GetLinksAsync(string userId);

        Task<bool> SaveLinkAsync(Link link);
        Task<bool> DeleteLinkAsync(Link link);
    }
}

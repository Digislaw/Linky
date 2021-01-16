using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Linky.DataAccessLayer.Repositories.Concrete
{
    public class LinkRepository : BaseRepository, ILinkRepository
    {
        public async Task<Link> GetLinkAsync(int id)
        {
            return await context.Links.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Link>> GetLinksAsync()
        {
            return await context.Links.ToListAsync();
        }

        public async Task<IEnumerable<Link>> GetLinksAsync(string userId)
        {
            return await context.Links
                .Where(l => l.ApplicationUserId == userId)
                .ToListAsync();
        }

        public async Task<bool> SaveLinkAsync(Link link)
        {
            if (link == null)
            {
                return false;
            }
               

            if (link.Id == default)
            {
                link.CreatedAt = DateTime.Now;
                context.Entry(link).State = EntityState.Added;
            }
            else
            {
                context.Entry(link).State = EntityState.Modified;
            }

            await context.SaveChangesAsync();

            /*
            try
            {
                if(link.Id == default)
                {
                    link.CreatedAt = DateTime.Now;
                    context.Entry(link).State = EntityState.Added;
                }
                else
                {
                    context.Entry(link).State = EntityState.Modified;
                }

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            */

            return true;
        }

        public async Task<bool> DeleteLinkAsync(Link link)
        {
            if (link == null)
                return false;

            context.Links.Remove(link);

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

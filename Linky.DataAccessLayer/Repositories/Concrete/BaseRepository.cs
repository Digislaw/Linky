using Linky.Entities;

namespace Linky.DataAccessLayer.Repositories.Concrete
{
    public class BaseRepository
    {
        protected AppDbContext context;

        public BaseRepository()
        {
            context = AppDbContext.Create();
        }
    }
}

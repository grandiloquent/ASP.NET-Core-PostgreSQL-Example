using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Models
{
    public class EFDataRepository : IDataRepository
    {
        private EfDatabaseContext context;

        public EFDataRepository(EfDatabaseContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Video> Videos => context.Videos;
    }
}
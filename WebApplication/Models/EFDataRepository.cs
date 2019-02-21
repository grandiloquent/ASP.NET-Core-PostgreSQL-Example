using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Models
{
    public class EFDataRepository : IDataRepository
    {
        private readonly EfDatabaseContext _context;

        public EFDataRepository(EfDatabaseContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Video> Videos => _context.Videos;

        public int CreateVideo(Video video)
        {
            _context.Add(new Video());
            return _context.SaveChanges();
        }
    }
}
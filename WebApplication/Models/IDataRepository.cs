using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Models
{
    public interface IDataRepository
    {
        IQueryable<Video> Videos { get; }

        int CreateVideo(Video video);
    }
}
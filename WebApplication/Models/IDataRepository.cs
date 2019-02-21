using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Models
{
    public interface IDataRepository
    {
        IQueryable<Video> Videos { get; }
    }
}
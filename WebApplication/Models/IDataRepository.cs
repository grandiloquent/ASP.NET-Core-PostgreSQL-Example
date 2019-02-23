using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public interface IDataRepository
    {
        Task<int> Unlike(long id);
        Task<int> Like(long id);
        void Delete(long id);
        IEnumerable<Video> GetAllVideos();
        int CreateVideo(Video video);
        Video GetVideo(long id);
        Task IncreaseWatchTimes(long id);
        IEnumerable<Video> GetLastVideos(int limit = 10);
        void UpdateVideo(Video changed, Video original = null);
    }
}
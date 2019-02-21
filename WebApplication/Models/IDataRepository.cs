using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Models
{
    public interface IDataRepository
    {
        void Delete(long id);
        IEnumerable<Video> GetAllVideos();
        int CreateVideo(Video video);
        Video GetVideo(long id);
        void UpdateVideo(Video changed, Video original=null);
    }
}
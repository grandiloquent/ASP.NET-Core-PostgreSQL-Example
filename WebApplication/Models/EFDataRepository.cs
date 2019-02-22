using System;
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


        public void Delete(long id)
        {
            _context.Videos.Remove(new Video {Id = id});
            _context.SaveChanges();
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return _context.Videos;
        }

        public IEnumerable<Video> GetLastVideos(int limit = 10)
        {
            return _context.Videos.OrderByDescending(i => i.UpdatedAt).Take(limit);
        }

        public void UpdateVideo(Video changed, Video original = null)
        {
            if (original == null)
            {
                original = _context.Videos.Find(changed.Id);
            }
            else
            {
                _context.Videos.Attach(original);
            }

            original.Id = changed.Id;
            original.Title = changed.Title;
            original.Cover = changed.Cover;
            original.Url = changed.Url;
            original.Thumbnail = changed.Thumbnail;
            original.WatchedCount = changed.WatchedCount;
            original.VoteUp = changed.VoteUp;
            original.VoteDown = changed.VoteDown; 
            original.UpdatedAt = DateTime.UtcNow;
            original.Duration = changed.Duration;
            original.Tags = changed.Tags;
            original.Width = changed.Width;
            original.Height = changed.Height;

            _context.SaveChanges();
        }

        public int CreateVideo(Video video)
        {
            _context.Add(video);
            return _context.SaveChanges();
        }

        public Video GetVideo(long id)
        {
            return _context.Videos.Find(id);
        }
    }
}
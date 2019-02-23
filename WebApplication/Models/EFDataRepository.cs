using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            var video = GetVideo(id);

            _context.Videos.Remove(video);

            if (video.VideoDetail != null)
            {
                _context.Remove(video.VideoDetail);
            }

            _context.SaveChanges();
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return _context.Videos.Include(v => v.Album).Include(v => v.VideoDetail);
        }

        public IEnumerable<Video> GetLastVideos(int limit = 10)
        {
            return _context.Videos.Include(v => v.Album).OrderByDescending(i => i.UpdatedAt).Take(limit);
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

            original.Title = changed.Title;
            original.Cover = changed.Cover;
            original.Url = changed.Url;
            original.Thumbnail = changed.Thumbnail;
            original.WatchedCount = changed.WatchedCount;
            original.VoteUp = changed.VoteUp;
            original.VoteDown = changed.VoteDown;
            original.UpdatedAt = DateTime.UtcNow;
            original.Tags = changed.Tags;

            original.VideoDetail.Width = changed.VideoDetail.Width;
            original.VideoDetail.Height = changed.VideoDetail.Height;
            original.VideoDetail.Duration = changed.VideoDetail.Duration;

            original.Album.Title = original.Album.Title;
            original.Album.Cover = original.Album.Cover;

            _context.SaveChanges();
        }

        public int CreateVideo(Video video)
        {
            video.Id = 0;
            Console.WriteLine(_context.Albums.Count());
            var album = _context.Albums.FirstOrDefault(i => i.Title == video.Album.Title);
            if (album != null)
            {
                video.AlbumId = album.Id;
                video.Album = null;
            }

            _context.Videos.Add(video);
            return _context.SaveChanges();
        }

        public Video GetVideo(long id)
        {
            return _context.Videos.Include(v => v.Album)
                .Include(v => v.VideoDetail)
                .First(v => v.Id == id);
        }
    }
}
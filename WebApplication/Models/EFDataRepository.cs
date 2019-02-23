using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


            _context.SaveChanges();
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return _context.Videos.Include(v => v.Album);
        }

        public async Task IncreaseWatchTimes(long id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video != null)
            {
                video.WatchedCount = video.WatchedCount + 1;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> Like(long id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null) return 0;
            video.VoteUp = video.VoteUp + 1;
            await _context.SaveChangesAsync();
            return video.VoteUp;

        }
        public async Task<int> Unlike(long id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null) return 0;
            video.VoteDown = video.VoteDown + 1;
            await _context.SaveChangesAsync();
            return video.VoteDown;

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

            original.Width = changed.Width;
            original.Height = changed.Height;
            original.Duration = changed.Duration;

            original.Album.Title = original.Album.Title;
            original.Album.Cover = original.Album.Cover;

            _context.SaveChanges();
        }

        public int CreateVideo(Video video)
        {
            video.Id = 0;
            var album = _context.Albums.FirstOrDefault(i => i.Title == video.Album.Title);
            if (album != null)
            {
                video.AlbumId = album.Id;
                video.Album = null;
            }
            /*else
           {
               _context.Albums.Add(video.Album);
               _context.SaveChanges();
           }*/

            if (video.VideoTags == null)
            {
                video.VideoTags = new List<VideoTag>();
            }

            foreach (var tag in video.Tags)
            {
                var old = _context.Tags.FirstOrDefault(v => v.Name == tag);
                if (old == null)
                {
                    video.VideoTags.Add(new VideoTag
                    {
                        Video = video, Tag = new Tag {Name = tag}
                    });
                }
                else
                {
                    video.VideoTags.Add(new VideoTag
                        {
                            Video = video, Tag = old
                        }
                    );
                }
            }

            _context.Videos.Add(video);
            return _context.SaveChanges();
        }

        public Video GetVideo(long id)
        {
            return _context.Videos.Include(v => v.Album)
                .Include(t => t.VideoTags)
                .ThenInclude(t => t.Tag)
                .First(v => v.Id == id);
        }
    }
}
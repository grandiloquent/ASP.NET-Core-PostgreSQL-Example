using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication.Shared;

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

        public Task<string> ExportDatabase()
        {
            return Task.Run(() =>
            {
                var items = _context
                    .Videos
                    //.Include(v=>v.Album)
                    .Include(v => v.VideoTags)
                    .ThenInclude(v => v.Tag);

                foreach (var item in items)
                {
                    if(item.VideoTags!=null)
                   item.Tags= item.VideoTags.Select(i => i.Tag.Name).ToList();

                    item.VideoTags = null;
                    
                }

                var value = JsonConvert.SerializeObject(items);
                var dst = Path.Combine(Directory.GetCurrentDirectory(),
                    "database" + DateTime.Now.ToString("-yyyy-MM-dd") + ".json");

                File.WriteAllText(dst, value, new UTF8Encoding());
                return dst;
            });
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

        public async Task<int> UpdateVideo(Video changed, Video original = null)
        {
            if (original == null)
            {
                original = _context.Videos.Find(changed.Id);
            }
            else
            {
                _context.Videos.Attach(original);
            }

            if (!string.IsNullOrWhiteSpace(changed.Cover)) original.Cover = changed.Cover;
            if (!string.IsNullOrWhiteSpace(changed.Thumbnail)) original.Thumbnail = changed.Thumbnail;
            if (!string.IsNullOrWhiteSpace(changed.Title)) original.Title = changed.Title;
            if (!string.IsNullOrWhiteSpace(changed.Url)) original.Url = changed.Url;
            if (!string.IsNullOrWhiteSpace(changed.Duration)) original.Duration = changed.Duration;


            if (changed.VoteDown > 0) original.VoteDown = changed.VoteDown;
            if (changed.VoteUp > 0) original.VoteUp = changed.VoteUp;
            if (changed.WatchedCount > 0) original.WatchedCount = changed.WatchedCount;
            if (changed.Height > 0) original.Height = changed.Height;
            if (changed.Width > 0) original.Width = changed.Width;


            if (changed.CreatedAt.Year != 1)
            {
                original.CreatedAt = changed.CreatedAt;
            }

            original.UpdatedAt = changed.UpdatedAt.Year == 1 ? DateTime.UtcNow : changed.UpdatedAt;

            if (changed.Album != null)
            {
                var album = await _context.Albums.FirstOrDefaultAsync(i => i.Title == changed.Album.Title);
                if (album != null)
                {
                    original.AlbumId = album.Id;
                }
                else
                {
                    original.Album = changed.Album;
                }
            }

            if (changed.Tags == null || changed.Tags.Count <= 0) return await _context.SaveChangesAsync();
            original.VideoTags.Clear();
            foreach (var tag in changed.Tags)
            {
                var old = await _context.Tags.FirstOrDefaultAsync(v => v.Name == tag) ?? new Tag {Name = tag};

                original.VideoTags.Add(new VideoTag
                    {
                        Video = original, Tag = old
                    }
                );
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateVideo(Video video)
        {
            video.Id = 0;
            var album = await _context.Albums.FirstOrDefaultAsync(i => i.Title == video.Album.Title);
            if (album != null)
            {
                video.AlbumId = album.Id;
            }

            if (video.VideoTags == null)
            {
                video.VideoTags = new List<VideoTag>();
            }

            foreach (var tag in video.Tags)
            {
                var old = await _context.Tags.FirstOrDefaultAsync(v => v.Name == tag);
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

            await _context.Videos.AddAsync(video);
            return await _context.SaveChangesAsync();
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
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Binders;
using WebApplication.Shared;

namespace WebApplication.Models
{
    public class Video
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public int WatchedCount { get; set; }
        public int VoteUp { get; set; }
        public int VoteDown { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long Duration { get; set; }

        [BindProperty(BinderType = typeof(SplitModelBinder))]
        public List<string> Tags { get; set; }

        public override string ToString()
        {
            return "\nId = " + Id +
                   "\nTitle = " + Title +
                   "\nCover = " + Cover +
                   "\nUrl = " + Url +
                   "\nThumbnail = " + Thumbnail +
                   "\nWatchedCount = " + WatchedCount +
                   "\nVoteUp = " + VoteUp +
                   "\nVoteDown = " + VoteDown +
                   "\nCreatedAt = " + CreatedAt +
                   "\nUpdatedAt = " + UpdatedAt +
                   "\nDuration = " + Duration +
                   "\nTags = " + Tags.ConcatenateLines();
        }
    }
}
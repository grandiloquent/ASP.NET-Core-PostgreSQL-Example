namespace WebApplication.Models
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WebApplication.Binders;
    using WebApplication.Shared;

    public class Video
    {
        public string Cover { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Duration { get; set; }
        public Album Album { get; set; }
        public int Height { get; set; }
        public long Id { get; set; }

        [BindProperty(BinderType = typeof(SplitModelBinder))]
        public List<string> Tags { get; set; }

        public string Thumbnail { get; set; }
        [Required] [StringLength(100)] public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required] [StringLength(512)] public string Url { get; set; }
        public int VoteDown { get; set; }
        public int VoteUp { get; set; }
        public int WatchedCount { get; set; }
        public int Width { get; set; }

        public override string ToString()
        {
            return
                "\nId = " + Id +
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
                "\nTags = " + Tags.ConcatenateLines() +
                "\nWidth = " + Width +
                "\nHeight = " + Height;
        }
    }
}
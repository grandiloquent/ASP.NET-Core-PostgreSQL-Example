using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public long Id { get; set; }

        public string Cover { get; set; }
        [BindNever]
        public DateTime CreatedAt { get; set; }

        [BindProperty(BinderType = typeof(SplitModelBinder))]
        public List<string> Tags { get; set; }

        public string Thumbnail { get; set; }
        [Required] [StringLength(100)] public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required] [StringLength(512)] public string Url { get; set; }
        public int VoteDown { get; set; }
        public int VoteUp { get; set; }
        public int WatchedCount { get; set; }

        public long AlbumId { get; set; }
        public Album Album { get; set; }

        public long VideoDetailId { get; set; }
        public VideoDetail VideoDetail { get; set; }
    }
}
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual List<VideoTag> VideoTags { get; set; }
    }
}
namespace WebApplication.Models
{
    public class VideoTag
    {
        public long VideoId { get; set; }
        public Video Video { get; set; }

        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
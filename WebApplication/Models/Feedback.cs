using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Feedback
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime CreateAt { get; set; }
        [Required] public string Content { get; set; }
    }
}
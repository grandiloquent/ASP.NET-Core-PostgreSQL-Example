using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication.Models
{
    public class Album
    {
        public long Id { get; set; }

        public string Cover { get; set; }
        public string Title { get; set; }
        public IEnumerable<Video> Videos { get; set; }
    }
}
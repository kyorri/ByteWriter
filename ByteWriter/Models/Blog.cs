using System.Collections.Generic;

namespace ByteWriter.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}

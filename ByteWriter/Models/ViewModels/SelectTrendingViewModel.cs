using ByteWriter.Models;
using System.Collections.Generic;

namespace ByteWriter.Models.ViewModels
{
    public class SelectTrendingViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public List<int> SelectedBlogIds { get; set; } = new List<int>();
    }
}

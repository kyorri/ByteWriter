using System.Collections.Generic;
using ByteWriter.Models;

namespace ByteWriter.Models.ViewModels
{
    public class FriendBlogViewModel
    {
        public IEnumerable<Blog> FriendBlogs { get; set; }
        public IDictionary<int, List<User>> BlogUsers { get; set; }
    }
}

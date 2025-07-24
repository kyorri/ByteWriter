using System.Collections.Generic;

namespace ByteWriter.Models.ViewModels
{
    public class BlogWithUsersViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IDictionary<int, List<User>> BlogUsers { get; set; }
    }
}

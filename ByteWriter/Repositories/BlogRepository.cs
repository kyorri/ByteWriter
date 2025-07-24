using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;

namespace ByteWriter.Repositories
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        public BlogRepository(BloggingContext bloggingContext)
            : base(bloggingContext)
        {
        }
    }
}

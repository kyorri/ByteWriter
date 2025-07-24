using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;

namespace ByteWriter.Repositories
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(BloggingContext bloggingContext)
            : base(bloggingContext)
        {
        }
    }
}

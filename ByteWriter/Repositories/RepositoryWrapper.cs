using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;

namespace ByteWriter.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BloggingContext _bloggingContext;

        private IBlogRepository? _blogRepository;
        private IUserRepository? _userRepository;
        private IAttachmentRepository? _attachmentRepository;
        private IArticleRepository? _articleRepository;
        private ITrendingBlogRepository? _trendingBlogRepository;
        public IBlogRepository BlogRepository
        {
            get
            {
                if (_blogRepository == null)
                {
                    _blogRepository = new BlogRepository(_bloggingContext);
                }

                return _blogRepository;
            }
        }
        public ITrendingBlogRepository TrendingBlogRepository
        {
            get
            {
                if (_trendingBlogRepository == null)
                {
                    _trendingBlogRepository = new TrendingBlogRepository(_bloggingContext);
                }

                return _trendingBlogRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_bloggingContext);
                }

                return _userRepository;
            }
        }

        public IAttachmentRepository AttachmentRepository
        {
            get
            {
                if (_attachmentRepository == null)
                {
                    _attachmentRepository = new AttachmentRepository(_bloggingContext);
                }

                return _attachmentRepository;
            }
        }
        public IArticleRepository ArticleRepository
        {
            get
            {
                if (_articleRepository == null)
                {
                    _articleRepository = new ArticleRepository(_bloggingContext);
                }

                return _articleRepository;
            }
        }

        public RepositoryWrapper(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        public void Save()
        {
            _bloggingContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _bloggingContext.SaveChangesAsync();
        }
    }
}

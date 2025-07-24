namespace ByteWriter.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IBlogRepository BlogRepository { get; }
        IUserRepository UserRepository { get; }
        IAttachmentRepository AttachmentRepository { get; }
        IArticleRepository ArticleRepository { get; }
        ITrendingBlogRepository TrendingBlogRepository { get; }


        void Save();
        Task SaveAsync();
    }
}

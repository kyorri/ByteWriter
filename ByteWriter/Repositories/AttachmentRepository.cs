using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;

namespace ByteWriter.Repositories
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(BloggingContext bloggingContext)
            : base(bloggingContext)
        {
        }
    }
}

using ByteWriter.Models;
using System.Linq.Expressions;

namespace ByteWriter.Services.Interfaces
{
    public interface IAttachmentService
    {
        IQueryable<Attachment> FindAll();
        IQueryable<Attachment> FindByCondition(Expression<Func<Attachment, bool>> expression);
        void Create(Attachment entity);
        void Update(Attachment entity);
        void Delete(Attachment entity);

        void Save();
        void SaveAsync();

    }
}

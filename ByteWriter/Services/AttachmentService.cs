using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using System.Linq.Expressions;

namespace ByteWriter.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AttachmentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IQueryable<Attachment> FindAll()
        {
            return _repositoryWrapper.AttachmentRepository.FindAll();
        }
        public IQueryable<Attachment> FindByCondition(Expression<Func<Attachment, bool>> expression)
        {
            return _repositoryWrapper.AttachmentRepository.FindByCondition(expression);
        }
        public void Create(Attachment entity)
        {
            _repositoryWrapper.AttachmentRepository.Create(entity);
        }
        public void Update(Attachment entity)
        {
            _repositoryWrapper.AttachmentRepository.Update(entity);
        }
        public void Delete(Attachment entity)
        {
            _repositoryWrapper.AttachmentRepository.Delete(entity);
        }

        public void Save()
        {
            _repositoryWrapper.Save();
        }

        public void SaveAsync()
        {
            _repositoryWrapper.SaveAsync();
        }
    }

}

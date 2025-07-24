using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using System.Linq.Expressions;

namespace ByteWriter.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IQueryable<User> FindAll()
        {
            return _repositoryWrapper.UserRepository.FindAll();
        }
        public IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(expression);
        }
        public void Create(User entity)
        {
            _repositoryWrapper.UserRepository.Create(entity);
        }
        public void Update(User entity)
        {
            _repositoryWrapper.UserRepository.Update(entity);
        }
        public void Delete(User entity)
        {
            _repositoryWrapper.UserRepository.Delete(entity);
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

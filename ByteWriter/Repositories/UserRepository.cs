using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ByteWriter.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BloggingContext bloggingContext)
            : base(bloggingContext)
        { }
    }
}

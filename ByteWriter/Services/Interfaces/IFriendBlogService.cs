using ByteWriter.Models;
using Microsoft.AspNetCore.Identity;

namespace ByteWriter.Services.Interfaces
{
    public interface IFriendBlogService
    {
        public Task<List<Blog>> GetFriendsBlogs(string userId);
    }
}

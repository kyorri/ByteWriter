using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ByteWriter.Services
{
    public class FriendBlogService : IFriendBlogService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;

        public FriendBlogService(UserManager<User> userManager, IRepositoryWrapper repositoryWrapper)
        {
            _userManager = userManager;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Blog>> GetFriendsBlogs(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var allUsers = await _userManager.Users.ToListAsync();

            var followers = allUsers.Where(u => u.Following.Contains(user));

            var following = user.Following;

            var friends = followers.Where(u => following.Contains(u));

            var friendsBlogs = friends.Select(friend => friend.Blog).ToList();

            return friendsBlogs;
        }
    }
}

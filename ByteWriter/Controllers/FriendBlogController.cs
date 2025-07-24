using ByteWriter.Models;
using ByteWriter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWriter.Controllers
{
    [Authorize]
    public class FriendBlogController : Controller
    {
        private readonly IFriendBlogService _friendBlogService;
        private readonly UserManager<User> _userManager;

        public FriendBlogController(IFriendBlogService friendBlogService, UserManager<User> userManager)
        {
            _friendBlogService = friendBlogService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var friendsBlogs = await _friendBlogService.GetFriendsBlogs(currentUser.Id);
                return View(friendsBlogs);
            }
            return Forbid();
        }

    }
}

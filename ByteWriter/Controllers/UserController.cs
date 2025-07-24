using ByteWriter.Models;
using ByteWriter.Models.ViewModels;
using ByteWriter.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWriter.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IArticleService _articleService;
        private readonly IBlogService _blogService;

        public UserController(UserManager<User> userManager, IArticleService articleService, IBlogService blogService)
        {
            _userManager = userManager;
            _articleService = articleService;
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View(new List<UserSearchViewModel>());
            }

            var users = await _userManager.Users
                .Where(u => u.UserName.StartsWith(query))
                .ToListAsync();

            var currentUser = await _userManager.GetUserAsync(User);
            var searchResults = users.Select(u => new UserSearchViewModel
            {
                UserName = u.UserName.Split('@')[0],
                Email = u.Email,
                Bio = u.Bio,
                IsFollowing = currentUser?.Following?.Any(f => f.Id == u.Id) ?? false,
                UserId = u.Id,
                IsCurrentUser = currentUser?.Id == u.Id
            }).ToList();

            return View(searchResults);
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Profile", new { username = user.UserName });
        }

        [HttpGet("Profile/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            bool isFollowing = currentUser?.Following?.Any(f => f.Id == user.Id) ?? false;

            var viewModel = new UserProfileViewModel
            {
                UserName = user.UserName.Split('@')[0],
                Email = user.Email,
                Bio = user.Bio,
                IsAdministrator = await _userManager.IsInRoleAsync(user, "Administrator"),
                StatusMessage = TempData["StatusMessage"] as string,
                IsFollowing = isFollowing,
                IsCurrentUser = currentUser?.Id == user.Id,
                UserId = user.Id
            };

            if (user.BlogId.HasValue)
            {
                var articles = await _articleService.FindByBlogIdAsync(user.BlogId.Value);
                viewModel.UserArticles = articles;
            }
            else
            {
                viewModel.UserArticles = new List<Article>();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Follow(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || currentUser.Id == userId)
            {
                return RedirectToAction("Index");
            }

            var userToFollow = await _userManager.FindByIdAsync(userId);

            if (userToFollow == null)
            {
                return RedirectToAction("Index");
            }

            currentUser.Following.Add(userToFollow);

            await _userManager.UpdateAsync(userToFollow);

            return RedirectToAction("Profile", new { username = userToFollow.UserName });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unfollow(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || currentUser.Id == userId)
            {
                return RedirectToAction("Index");
            }

            var userToUnfollow = await _userManager.FindByIdAsync(userId);

            if (userToUnfollow == null)
            {
                return RedirectToAction("Index");
            }

            currentUser.Following.Remove(userToUnfollow);

            await _userManager.UpdateAsync(userToUnfollow);

            return RedirectToAction("Profile", new { username = userToUnfollow.UserName });
        }


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditBioViewModel { Bio = user.Bio };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                user.Bio = model.Bio;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["StatusMessage"] = "Bio updated successfully!";
                    return RedirectToAction("Profile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var article = await _articleService.FindByCondition(a => a.ArticleID == id).FirstOrDefaultAsync();
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Friends()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return null;
            }

            var allUsers = await _userManager.Users.ToListAsync();

            var followers = allUsers.Where(u => u.Following.Contains(user));
            var following = user.Following;

            var friends = followers.Where(u => following.Contains(u)).ToList();

            return View(friends);
        }

    }
}

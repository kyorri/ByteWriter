using ByteWriter.Models;
using ByteWriter.Models.ViewModels;
using ByteWriter.Services;
using ByteWriter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ByteWriter.Controllers
{
    public class TrendingBlogController : Controller
    {
        private readonly ITrendingBlogService _trendingBlogService;
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;

        public TrendingBlogController(ITrendingBlogService trendingBlogService, IUserService userService, IBlogService blogService)
        {
            _trendingBlogService = trendingBlogService;
            _userService = userService;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var trendingBlogs = _trendingBlogService.GetTrendingBlogs();
            var blogs = new List<Blog>();

            foreach (var trendingBlog in trendingBlogs)
            {
                var blog = await _blogService.FindByCondition(b => b.BlogID == trendingBlog.BlogId)
                                              .FirstOrDefaultAsync();
                if (blog != null)
                {
                    blogs.Add(blog);
                }
            }

            var blogUsers = new Dictionary<int, List<User>>();

            foreach (var blog in blogs)
            {
                var users = await _userService.FindByCondition(u => u.BlogId == blog.BlogID)
                                              .ToListAsync();
                blogUsers[blog.BlogID] = users.ToList();
            }

            var viewModel = new BlogWithUsersViewModel
            {
                Blogs = blogs,
                BlogUsers = blogUsers
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SelectTrending()
        {
            var model = new SelectTrendingViewModel();

            var blogs = await _blogService.FindAll().ToListAsync();
            model.Blogs = blogs;

            var trendingBlogs = _trendingBlogService.GetTrendingBlogs().Select(tb => tb.BlogId).ToList();
            model.SelectedBlogIds = trendingBlogs;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectTrending(SelectTrendingViewModel model)
        {
            var allTrending = _trendingBlogService.GetTrendingBlogs().Select(tb => tb.BlogId).ToList();

            foreach (var blogId in model.SelectedBlogIds)
            {
                if (!allTrending.Contains(blogId))
                {
                    _trendingBlogService.AddTrendingBlog(blogId);
                }
            }

            foreach (var blogId in allTrending)
            {
                if (!model.SelectedBlogIds.Contains(blogId))
                {
                    _trendingBlogService.RemoveTrendingBlog(blogId);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        

    }
}
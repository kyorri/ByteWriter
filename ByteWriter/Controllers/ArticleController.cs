using ByteWriter.Models;
using ByteWriter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWriter.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;

        public ArticleController(IArticleService articleService, UserManager<User> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var articles = await _articleService.FindByUserIdAsync(currentUser.Id);
            return View(articles);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            currentUser = await _userManager.Users
                .Include(u => u.Blog)
                .FirstOrDefaultAsync(u => u.Id == currentUser.Id);

            if (currentUser.Blog == null)
            {
                return BadRequest("User doesn't have a blog.");
            }

            article.ArticleTimestamp = DateTime.UtcNow;
            _articleService.Create(article);

            var blog = currentUser.Blog;
            blog.Articles.Add(article);

            _articleService.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.FindByCondition(a => a.ArticleID == id).FirstOrDefaultAsync();
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _articleService.FindByCondition(a => a.ArticleID == id).FirstOrDefaultAsync();

            if (article == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            _articleService.Delete(article);
            _articleService.Save();
            return RedirectToAction(nameof(Index));
        }


        private bool ArticleExists(int id)
        {
            return _articleService.FindByCondition(a => a.ArticleID == id).FirstOrDefault() != null;
        }
    }
}

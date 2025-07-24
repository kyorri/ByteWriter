using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ByteWriter.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public RegisterService(
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager,
            IRepositoryWrapper repositoryWrapper
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task SetUserNameAsync(User user, string email, CancellationToken cancellationToken)
        {
            await _userStore.SetUserNameAsync(user, email, cancellationToken);
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            await _emailStore.SetEmailAsync(user, email, cancellationToken);
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return await _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public async Task SignInAsync(User user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public bool RequireConfirmedAccount => _userManager.Options.SignIn.RequireConfirmedAccount;

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }

        public async Task AddUserToDefaultRoleAsync(User user)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }
        public async Task CreateBlogForUserAsync(User user)
        {
            if (user == null)
            {
                return;
            }

            var blog = new Blog
            {
                BlogTitle = $"{user.UserName}'s Blog",
                BlogDescription = $"This is the blog for {user.UserName}"
            };

            user.Blog = blog;

            await _repositoryWrapper.SaveAsync();

        }
    }
}

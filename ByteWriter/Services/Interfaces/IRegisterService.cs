using ByteWriter.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteWriter.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task AddUserToDefaultRoleAsync(User user);
        Task SetUserNameAsync(User user, string email, CancellationToken cancellationToken);
        Task SetEmailAsync(User user, string email, CancellationToken cancellationToken);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task SignInAsync(User user);
        bool RequireConfirmedAccount { get; }
        Task CreateBlogForUserAsync(User user);
    }
}

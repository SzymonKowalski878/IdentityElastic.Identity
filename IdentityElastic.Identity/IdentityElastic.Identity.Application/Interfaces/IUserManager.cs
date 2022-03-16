using Feree.ResultType;
using Feree.ResultType.Results;
using IdentityElastic.Identity.Domain.Models;

namespace IdentityElastic.Identity.Application.Interfaces
{
    public interface IUserManager
    {
        Task<IResult<Unit>> ValidatePassword(Guid userId, string password, CancellationToken cancellationToken);
        Task<IResult<Unit>> ChangePassword(ApplicationUser user, string password, CancellationToken token);
        Task<IResult<ApplicationUser>> AddUserAsync(ApplicationUser user, CancellationToken token);
        Task<IResult<ApplicationUser>> AddUserToRoleAsync(ApplicationUser user, string role, CancellationToken token);
    }
}

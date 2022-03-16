using Feree.ResultType;
using Feree.ResultType.Results;
using IdentityElastic.Identity.Domain.Models;

namespace IdentityElastic.Identity.Application.Interfaces
{
    public interface IPasswordValidator
    {
        Task<IResult<Unit>> ValidatePassword(ApplicationUser user, string password, CancellationToken token);
    }
}

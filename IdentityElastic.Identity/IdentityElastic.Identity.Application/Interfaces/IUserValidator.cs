using Feree.ResultType;
using Feree.ResultType.Results;

namespace IdentityElastic.Identity.Application.Interfaces
{
    public interface IUserValidator
    {
        Task<IResult<Unit>> ValidateEmailExists(string email, CancellationToken token);
    }
}

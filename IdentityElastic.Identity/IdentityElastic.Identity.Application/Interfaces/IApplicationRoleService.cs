using Feree.ResultType;
using Feree.ResultType.Results;

namespace IdentityElastic.Identity.Application.Interfaces
{
    public interface IApplicationRoleService
    {
        Task<IResult<Unit>> CheckIfWorkerRoleExists(string roleName, CancellationToken cancellationToken);
    }
}

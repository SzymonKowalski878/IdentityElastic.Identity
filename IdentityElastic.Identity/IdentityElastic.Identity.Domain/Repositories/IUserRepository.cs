using Feree.ResultType.Results;
using IdentityElastic.Identity.Domain.Models;

namespace IdentityElastic.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IResult<ApplicationUser>> Get(Guid id, CancellationToken cancellationToken);
        Task<IResult<bool>> CheckUniqueEmail(string email, CancellationToken cancellationToken);
    }
}

using Feree.ResultType.Converters;
using Feree.ResultType.Factories;
using Feree.ResultType.Results;
using IdentityElastic.Identity.Domain.Models;
using IdentityElastic.Identity.Domain.Repositories;
using IdentityElastic.Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace IdentityElastic.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult<bool>> CheckUniqueEmail(string email, CancellationToken cancellationToken) =>
            await _context.Users.Where(x => x.NormalizedEmail == email.ToUpper()).AnyAsync().AsResultAsync();


        public Task<IResult<ApplicationUser>> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = _context.Users.FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                return ResultFactory.CreateFailureAsync<ApplicationUser>("User not found");
            }
            return ResultFactory.CreateSuccessAsync(result);
        }
    }
}

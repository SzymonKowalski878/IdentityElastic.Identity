using Feree.ResultType;
using Feree.ResultType.Factories;
using Feree.ResultType.Operations;
using Feree.ResultType.Results;
using IdentityElastic.Identity.Application.Interfaces;
using IdentityElastic.Identity.Domain.Errors;
using IdentityElastic.Identity.Domain.Models;
using IdentityElastic.Identity.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IdentityElastic.Identity.Application.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManager(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IResult<ApplicationUser>> AddUserAsync(ApplicationUser user, CancellationToken token)
        {
            var result = await _userManager.CreateAsync(user);
            return result.Succeeded
                ? ResultFactory.CreateSuccess(user)
                : ResultFactory.CreateFailure<ApplicationUser>(
                    DatabaseOperationError.FromIdentityErrors(result.Errors));
        }


        public async Task<IResult<ApplicationUser>> AddUserToRoleAsync(ApplicationUser user, string role, CancellationToken token)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded
                ? ResultFactory.CreateSuccess(user)
                : ResultFactory.CreateFailure<ApplicationUser>(
                    DatabaseOperationError.FromIdentityErrors(result.Errors));
        }

        public async Task<IResult<Unit>> ChangePassword(ApplicationUser user, string password, CancellationToken token)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, password);
            return result.Succeeded
                ? ResultFactory.CreateSuccess()
                : ResultFactory.CreateFailure(string.Join('\n', result.Errors.Select(x => $"{x.Code}: {x.Description}")));
        }

        public Task<IResult<Unit>> ValidatePassword(Guid userId, string password, CancellationToken cancellationToken) =>
            _userRepository.Get(userId, cancellationToken)
            .BindAnyAsync(user => _userManager.CheckPasswordAsync(user, password))
                .BindAsync(isValid => isValid
                    ? ResultFactory.CreateSuccess()
                    : ResultFactory.CreateFailure("Invalid password"));

    }
}

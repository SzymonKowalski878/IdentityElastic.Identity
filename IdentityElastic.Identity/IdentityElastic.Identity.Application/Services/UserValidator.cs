using Feree.ResultType;
using Feree.ResultType.Factories;
using Feree.ResultType.Results;
using IdentityElastic.Identity.Application.Factory;
using IdentityElastic.Identity.Application.Interfaces;
using IdentityElastic.Identity.Domain.Repositories;

namespace IdentityElastic.Identity.Application.Services
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IResult<Unit>> ValidateEmailExists(string email, CancellationToken token) =>
            _userRepository.CheckUniqueEmail(email, token)
                .BindAsync(exists => !exists
                    ? ResultFactory.CreateSuccess()
                    : ResultFactory.CreateFailure(ValidationErrorFactory.Create("User already exists", "email")));


    }
}

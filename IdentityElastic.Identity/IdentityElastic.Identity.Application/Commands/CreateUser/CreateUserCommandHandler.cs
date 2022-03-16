using Feree.ResultType.Results;
using IdentityElastic.Identity.Application.Interfaces;
using IdentityElastic.Identity.Client.Models;

namespace IdentityElastic.Identity.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : MediatR.IRequestHandler<CreateUserCommand, IResult<CreateUserResponseDto>>
    {
        private readonly IUserManager _userManager;

        public CreateUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<IResult<CreateUserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

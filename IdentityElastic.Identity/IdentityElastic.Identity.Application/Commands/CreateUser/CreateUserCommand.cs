using Feree.ResultType.Results;
using IdentityElastic.Identity.Client.Models;

namespace IdentityElastic.Identity.Application.Commands.CreateUser
{
    public record CreateUserCommand(string FirstName, string Surname, string PhoneNumber, string Email, string Password)
        : MediatR.IRequest<IResult<CreateUserResponseDto>>;
}

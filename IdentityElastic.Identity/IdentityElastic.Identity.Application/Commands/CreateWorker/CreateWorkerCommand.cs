using Feree.ResultType.Results;
using IdentityElastic.Identity.Client.Models;
using MediatR;

namespace IdentityElastic.Identity.Application.Commands.CreateWorker
{
    public record CreateWorkerCommand(string Email, string Password, string FirstName, string LastName, string PhoneNumber, string Role)
        : IRequest<IResult<CreateUserResponseDto>>;
}

using Feree.ResultType.Results;
using IdentityElastic.Identity.Client.Models;

namespace IdentityElastic.Identity.Application.Commands.Login
{
    public record LoginCommand(string Login, string Password)
        : MediatR.IRequest<IResult<LoginResponseDto>>;
}

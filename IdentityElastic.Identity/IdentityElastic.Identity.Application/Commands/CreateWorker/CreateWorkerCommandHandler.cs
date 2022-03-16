using Feree.ResultType;
using Feree.ResultType.Converters;
using Feree.ResultType.Results;
using FluentValidation;
using IdentityElastic.Identity.Application.Interfaces;
using IdentityElastic.Identity.Application.Mapper;
using IdentityElastic.Identity.Client.Models;
using IdentityElastic.Identity.Domain.Models;

namespace IdentityElastic.Identity.Application.Commands.CreateWorker
{
    public class CreateWorkerCommandHandler : MediatR.IRequestHandler<CreateWorkerCommand, IResult<CreateUserResponseDto>>
    {
        private readonly IUserManager _userManager;
        private readonly IApplicationRoleService _applicationRoleService;
        private readonly IUserValidator _userValidator;
        private readonly IValidator<CreateWorkerCommand> _workerValidator;

        public CreateWorkerCommandHandler(IUserManager userManager,
            IApplicationRoleService applicationRoleService,
            IUserValidator userValidator,
            IValidator<CreateWorkerCommand> workerValidator)
        {
            _userManager = userManager;
            _applicationRoleService = applicationRoleService;
            _userValidator = userValidator;
            _workerValidator = workerValidator;
        }

        public Task<IResult<CreateUserResponseDto>> Handle(CreateWorkerCommand request, CancellationToken cancellationToken) =>
            Validate(request, cancellationToken)
                .BindAsync(_ => Create(request, cancellationToken));

        private Task<IResult<Unit>> Validate(CreateWorkerCommand request, CancellationToken cancellationToken) =>
            _workerValidator.Validate(request).AsResult()
                .BindAsync(_ => _applicationRoleService.CheckIfWorkerRoleExists(request.Role, cancellationToken)
                    .BindAsync(_ => _userValidator.ValidateEmailExists(request.Email, cancellationToken)));


        public Task<IResult<CreateUserResponseDto>> Create(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var user = ApplicationUserFactory.GetWorkerApplicationUser(request);

            return _userManager.AddUserAsync(user, cancellationToken)
                .BindAsync(user => _userManager.AddUserToRoleAsync(user, "Worker", cancellationToken)
                    .BindAsync(user => _userManager.ChangePassword(user, request.Password, cancellationToken)
                        .BindAsync(_ => user.AsResult())
                        .BindAsync(user => MapResponse(user).AsResult())));
        }

        private static CreateUserResponseDto MapResponse(ApplicationUser user) => ApplicationUserMapper.GetResponse(user);
    }
}

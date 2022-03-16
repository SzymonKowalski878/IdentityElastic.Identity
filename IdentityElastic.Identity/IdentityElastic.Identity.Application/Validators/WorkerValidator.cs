using FluentValidation;
using IdentityElastic.Identity.Application.Commands.CreateWorker;
using System.Text.RegularExpressions;

namespace IdentityElastic.Identity.Application.Validators
{
    public class WorkerValidator : AbstractValidator<CreateWorkerCommand>
    {
        public bool ValidatePasswordStrength(string password)
        {
            var pattern = @"^.*((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$";

            return Regex.IsMatch(password,
                pattern,
                RegexOptions.ECMAScript);
        }

        public WorkerValidator()
        {
            RuleFor(m => m.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .WithMessage("Error during email validation");

            RuleFor(m => m.FirstName)
            .NotEmpty()
            .MaximumLength(30)
            .WithMessage("Failure to validate firstname");

            RuleFor(m => m.LastName)
            .NotEmpty()
            .MaximumLength(30)
            .WithMessage("Failure to validate surname");
        }
    }
}

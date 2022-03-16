using IdentityElastic.Identity.Application.Errors;

namespace IdentityElastic.Identity.Application.Factory
{
    public static class ValidationErrorFactory
    {
        public static ValidationError Create(string message, string? propertyName) =>
            new ValidationError("Invalid data", new ValidationError.Error[]
            {
                new(message,null,propertyName)
            });
    }
}

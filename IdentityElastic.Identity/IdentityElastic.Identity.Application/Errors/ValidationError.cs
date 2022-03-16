using Feree.ResultType.Results;

namespace IdentityElastic.Identity.Application.Errors
{
    public record ValidationError(string Message, IEnumerable<ValidationError.Error>? Errors = null) : IError
    {
        public record Error(string Message, string? Code = null, string? PropertyName = null);
    }
}

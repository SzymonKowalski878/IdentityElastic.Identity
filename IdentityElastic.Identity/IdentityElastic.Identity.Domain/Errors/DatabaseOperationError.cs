using Feree.ResultType.Results;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace IdentityElastic.Identity.Domain.Errors
{
    public record DatabaseOperationError(string Message) : IError
    {
        public static DatabaseOperationError FromIdentityErrors(IEnumerable<IdentityError> errors)
        {
            var sb = new StringBuilder();
            foreach (var e in errors)
                sb.Append($"{e.Code}: {e.Description}");

            return new DatabaseOperationError(sb.ToString());
        }
    }
}

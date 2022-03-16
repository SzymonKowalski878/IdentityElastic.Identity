using IdentityElastic.Identity.Client.Models;
using IdentityElastic.Identity.Domain.Models;

namespace IdentityElastic.Identity.Application.Mapper
{
    public static class ApplicationUserMapper
    {

        public static CreateUserResponseDto GetResponse(ApplicationUser user) =>
        new(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber
        );
    }
}

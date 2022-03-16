using IdentityElastic.Identity.Application.Commands.CreateWorker;
using IdentityElastic.Identity.Domain.Models;

namespace IdentityElastic.Identity.Application.Mapper
{
    public static class ApplicationUserFactory
    {
        public static ApplicationUser GetWorkerApplicationUser(CreateWorkerCommand commmad)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = commmad.Email,
                Email = commmad.Email,
                FirstName = commmad.FirstName,
                LastName = commmad.LastName,
                PhoneNumber = commmad.PhoneNumber,
                EmailConfirmed = true
            };

            return applicationUser;
        }
    }
}

using IdentityElastic.Security;

namespace IdentityElastic.Identity.Application.Helpers
{
    public static class RoleHelper
    {
        public static bool CheckIfWorkerRole(string roleName) =>
            roleName == UserRoleNames.Worker;

    }
}

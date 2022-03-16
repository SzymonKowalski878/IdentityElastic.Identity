using Feree.ResultType;
using Feree.ResultType.Factories;
using Feree.ResultType.Results;
using IdentityElastic.Identity.Application.Factory;
using IdentityElastic.Identity.Application.Helpers;
using IdentityElastic.Identity.Application.Interfaces;
using IdentityElastic.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityElastic.Identity.Application.Services
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IResult<Unit>> CheckIfWorkerRoleExists(string roleName, CancellationToken cancellationToken)
        {
            if (!RoleHelper.CheckIfWorkerRole(roleName))
            {
                return ResultFactory.CreateFailure(
                    ValidationErrorFactory.Create("Not supported role name", null));
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
            }

            return ResultFactory.CreateSuccess();
        }

        private Task<bool> RoleExists(string roleName, CancellationToken cancellationToken) =>
            _roleManager.RoleExistsAsync(roleName);
    }
}

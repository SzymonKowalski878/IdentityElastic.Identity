using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityElastic.Identity.Application.ConfigSection;
using IdentityElastic.Security;
using ISClient = Duende.IdentityServer.Models.Client;

namespace IdentityElastic.Identity.WebApi.Configuration
{
    internal class IDS_Consts
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = ApiScopesNames.DefaultScope,
                    UserClaims =
                    {
                        UserClaimsNames.UserId,
                        UserClaimsNames.UserName,
                        UserClaimsNames.FirstName,
                        UserClaimsNames.LastName,
                        UserClaimsNames.Email,
                        UserClaimsNames.Role,
                        UserClaimsNames.IsActivated
                    },
                    DisplayName = "User Management API",
                    Description = "API for managing users and accounts",
                    Enabled = true,
                    Scopes = { ApiScopesNames.DefaultScope }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(ApiScopesNames.DefaultScope, "Api for managing accounts")
            };

        public static IEnumerable<ISClient> Clients(ApiClientsConfiguration clientsConfig, TimeSpan tokenLifetime) =>
            new List<ISClient>
            {
                new ISClient
                {
                    ClientId = clientsConfig.M2M.ClientId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(clientsConfig.M2M.ClientSecret.Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        ApiScopesNames.DefaultScope
                    }
                },
                new ISClient
                {
                    ClientId = clientsConfig.Interactive.ClientId,
                    ClientSecrets = { new Secret(clientsConfig.Interactive.ClientSecret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireConsent = false,
                    AllowedScopes = new List<string>
                    {
                        ApiScopesNames.DefaultScope,
                    },
                    AccessTokenLifetime = (int)tokenLifetime.TotalSeconds,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                }
            };
    }
}

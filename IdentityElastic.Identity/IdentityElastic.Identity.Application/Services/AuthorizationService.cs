using Feree.ResultType;
using Feree.ResultType.Converters;
using Feree.ResultType.Factories;
using Feree.ResultType.Results;
using IdentityElastic.Identity.Application.ConfigSection;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IdentityElastic.Identity.Application.Services
{
    public class AuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiClientsConfiguration _config;
        private readonly string _authority;

        public AuthorizationService(IHttpClientFactory httpClientFactory,
            IConfiguration config,
            IOptions<ApiClientsConfiguration> apiOptions)
        {
            _httpClient = httpClientFactory.CreateClient();
            _config = apiOptions.Value;
            _authority = config["Environment:internalAuthorityUrl"];
        }

        public async Task<IResult<Unit>> RevokeToken(string refreshToken, CancellationToken cancellationToken)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _authority,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                    ValidateIssuerName = false
                }
            }, cancellationToken);

            if (disco.IsError)
            {
                return HandleError<Unit>(disco);
            }

            var revokeResponse = await _httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = disco.RevocationEndpoint,
                ClientId = _config.Interactive.ClientId,
                ClientSecret = _config.Interactive.ClientSecret,
                Token = refreshToken,
                TokenTypeHint = "refresh_token"
            }, cancellationToken);

            if (revokeResponse.IsError)
            {
                return HandleError<Unit>(revokeResponse);
            }

            return ResultFactory.CreateSuccess();
        }

        public async Task<IResult<TokenClient>> GetTokenClient()
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _authority,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                    ValidateIssuerName = false
                }
            });

            if (disco.IsError)
            {
                return HandleError<TokenClient>(disco);
            }

            var tokenClient = new TokenClient(_httpClient, new TokenClientOptions
            {
                Address = disco.TokenEndpoint,
                ClientId = _config.Interactive.ClientId,
                ClientSecret = _config.Interactive.ClientSecret
            });

            return tokenClient.AsResult();
        }

        private IResult<T> HandleError<T>(ProtocolResponse tokenResponse)
        {
            return ResultFactory.CreateFailure<T>(tokenResponse.Error);
        }
    }
}

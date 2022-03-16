namespace IdentityElastic.Identity.Client.Models
{
    public record TokensDto(
    string AccessToken,
    string RefreshToken,
    string TokenType,
    int ExpiresIn);
}

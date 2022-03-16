namespace IdentityElastic.Identity.Application.Commands.Login
{
    public record Token(
    string AccessToken,
    string RefreshToken,
    string TokenType,
    int ExpiresIn);
}

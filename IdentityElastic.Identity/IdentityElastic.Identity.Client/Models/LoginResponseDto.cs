namespace IdentityElastic.Identity.Client.Models
{
    public record LoginResponseDto(
    TokensDto Tokens,
    string FirstName,
    string LastName,
    string Email,
    bool ActivationRequired,
    bool EmailChangeRequired,
    bool PasswordChangeRequired,
    bool TermsOfServiceApprovalRequired);
}

namespace IdentityElastic.Identity.Client.Models
{
    public record CreateUserResponseDto(Guid UserId, string FirstName, string LastName, string Email, string PhoneNumber);
}

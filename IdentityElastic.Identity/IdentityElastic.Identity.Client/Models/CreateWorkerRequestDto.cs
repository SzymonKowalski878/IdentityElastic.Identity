namespace IdentityElastic.Identity.Client.Models
{
    public record CreateWorkerRequestDto(string Email, string Password, string FirstName, string LastName, string PhoneNumber, string Role);
}

namespace IdentityElastic.Identity.Application.ConfigSection
{
    public class ApiClientsConfiguration
    {
        public static string SectionName = nameof(ApiClientsConfiguration);

        public ApiClient M2M { get; set; } = new();
        public ApiClient Interactive { get; set; } = new();

        public class ApiClient
        {
            public string ClientId { get; set; } = "";
            public string ClientSecret { get; set; } = "";
        }
    }
}

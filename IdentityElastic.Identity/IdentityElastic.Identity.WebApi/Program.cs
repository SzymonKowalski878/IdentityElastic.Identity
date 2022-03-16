using IdentityElastic.Identity.WebApi;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetupConfiguration(builder.Configuration, builder.Environment)
    .Build();

bool isIntegrationTests = builder.Environment.IsEnvironment("IntegrationTests");

builder
    .Services
    .SetupServices(configuration, isIntegrationTests);

builder
    .Host
    .SetupHost();

var app = builder.Build();

await app
    .SetupApplication()
    .RunAsync();
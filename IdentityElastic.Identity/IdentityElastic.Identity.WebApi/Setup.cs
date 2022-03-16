using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityElastic.Identity.Application;
using IdentityElastic.Identity.Domain.Models;
using IdentityElastic.Identity.Infrastructure.Database;
using IdentityElastic.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IdentityElastic.Identity.WebApi
{
    public static class Setup
    {
        public static IConfigurationBuilder SetupConfiguration(
        this IConfigurationBuilder builder,
        ConfigurationManager configManager,
        IWebHostEnvironment environment)
        {
            builder
                .AddConfiguration(configManager)
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();


            return builder;
        }

        public static void SetupServices(this IServiceCollection services, IConfigurationRoot config, bool isIntegrationTests)
        {
            services.AddControllers()
                .AddNewtonsoftJson(config =>
                {
                    var settings = config.SerializerSettings;
                    settings.Converters.Add(new StringEnumConverter());
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.Formatting = Formatting.None;

                    settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            services.AddSingleton<IConfiguration>(config);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Diuna.Identity", Version = "v1" });
                c.CustomSchemaIds(type => type.FullName);
            });

            ConfigureEntityFramework(services, config, isIntegrationTests);

            ConfigureIdentity(services, config);
        }

        private static void ConfigureEntityFramework(IServiceCollection services, IConfigurationRoot config, bool isIntegrationTests)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (isIntegrationTests)
                {
                    options.UseInMemoryDatabase("InMemory");
                }
                else
                {
                    options.UseSqlServer(connectionString, c => c.EnableRetryOnFailure()).EnableSensitiveDataLogging();
                }
            });
        }

        public static void SetupHost(this ConfigureHostBuilder host)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule<MediatorModule>();
                builder.RegisterModule<ApplicationModule>();
            });
        }

        private static void ConfigureIdentity(IServiceCollection services, IConfigurationRoot config)
        {
            var authorityUrl = config["Environment:AuthorityUrl"];

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = authorityUrl;
                o.Audience = ApiScopesNames.DefaultScope;
                o.RequireHttpsMetadata = false;
            });
        }

        public static WebApplication SetupApplication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Diuna.Identity v1");
            });

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}

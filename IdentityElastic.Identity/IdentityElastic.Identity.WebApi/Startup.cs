using Autofac;
using IdentityElastic.Identity.Application;
using IdentityElastic.Identity.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace IdentityElastic.Identity.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private IWebHostEnvironment _webHostEnv { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnv)
        {
            Configuration = configuration;
            _webHostEnv = webHostEnv;
        }


        public void ConfigureServices(IServiceCollection services)
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Diuna.Identity", Version = "v1" });
                c.CustomSchemaIds(type => type.FullName);
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var inMemoryRoot = new InMemoryDatabaseRoot();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (_webHostEnv.EnvironmentName == "IntegrationTests")
                {
                    options.UseInMemoryDatabase("InMemory");
                }
                else
                {
                    options.UseSqlServer(connectionString, c => c.EnableRetryOnFailure()).EnableSensitiveDataLogging();
                }
            });

            services.AddMediatR(Assembly.GetEntryAssembly());

            //var builder = new ContainerBuilder();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityElastic.Identity");
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.RegisterModule<ApplicationModule>();
        }
    }
}

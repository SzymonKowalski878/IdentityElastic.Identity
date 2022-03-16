using Autofac;
using IdentityElastic.Identity.Application.Commands.CreateWorker;
using IdentityElastic.Identity.Application.Managers;
using IdentityElastic.Identity.Application.Services;
using IdentityElastic.Identity.Application.Validators;
using IdentityElastic.Identity.Infrastructure.Repositories;

namespace IdentityElastic.Identity.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterValidators(builder);
            RegisterQueryHandlers(builder);
            RegisterCommandHandlers(builder);
            RegisterDomainEventHandlers(builder);
            RegisterValidations(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationRoleService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces();
        }

        private static void RegisterValidators(ContainerBuilder builder)
        {
            builder.RegisterType<UserValidator>().AsImplementedInterfaces();
            builder.RegisterType<WorkerValidator>().AsImplementedInterfaces();
        }

        private static void RegisterQueryHandlers(ContainerBuilder builder)
        {
        }

        private static void RegisterCommandHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<CreateWorkerCommandHandler>().AsImplementedInterfaces();
        }

        private static void RegisterDomainEventHandlers(ContainerBuilder builder)
        {
        }

        private static void RegisterValidations(ContainerBuilder builder)
        {
        }
    }
}

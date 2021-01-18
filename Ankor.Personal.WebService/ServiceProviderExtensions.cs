using Ankor.Personal.WebService.Interfaces;
using Ankor.Personal.WebService.Model.Demos;
using Ankor.Personal.WebService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Ankor.Personal.WebService
{
    /// <summary>
    /// Extension methods for service collection.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Add autorizing services.
        /// </summary>
        /// <param name="services">service collection.</param>
        /// <returns>updated service collection.</returns>
        public static IServiceCollection AddAutorizingServices(this IServiceCollection services)
        {
            services.AddSingleton(
                typeof(IDBEntityService),
                typeof(DBEntityService));

            services.AddSingleton(
                typeof(IPasswordHasher),
                typeof(PasswordHasher));

            services.AddSingleton(
                typeof(IEmailSender),
                typeof(EmailSender));

            services.AddSingleton(
                typeof(IAuthorizingService),
                typeof(AuthorizingService));

            services.AddSingleton(
                typeof(IProfileService),
                typeof(ProfileService));

            services.AddSingleton(
                typeof(IDeviceService),
                typeof(DeviceService));

            services.AddSingleton(
                typeof(IMeasuresService),
                typeof(MeasuresService));

            services.AddSingleton(
                typeof(IEventsService),
                typeof(EventsService));

            services.AddSingleton(
                typeof(IHelpService),
                typeof(HelpService));

            return services;
        }

        /// <summary>
        /// Add logger service.
        /// </summary>
        /// <param name="services">service collection.</param>
        /// <param name="pathToLog">path to log.</param>
        /// <returns>updated service collection.</returns>
        public static IServiceCollection AddLoggerService(
            this IServiceCollection services,
            string pathToLog)
        {
            var logger = new LoggerConfiguration()
               .WriteTo.RollingFile(pathToLog)
               .CreateLogger();

            return services.AddSingleton(typeof(ILogger), logger);
        }

        /// <summary>
        /// Add demo repos.
        /// </summary>
        /// <param name="services">service collection.</param>
        /// <returns>updated service collection.</returns>
        public static IServiceCollection AddDemoRepositories(this IServiceCollection services)
        {
            services.AddSingleton(typeof(DemoDevicesRepository));
            services.AddSingleton(typeof(DemoFAQsRepository));
            services.AddSingleton(typeof(DemoEventsRepository));

            return services;
        }
    }
}

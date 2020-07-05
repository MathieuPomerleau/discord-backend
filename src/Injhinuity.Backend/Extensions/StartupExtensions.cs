using Google.Api.Gax;
using Google.Cloud.Firestore;
using Injhinuity.Backend.Core.Configuration;
using Injhinuity.Backend.Firestore;
using Injhinuity.Backend.Repositories.Firestore;
using Injhinuity.Backend.Repositories.Interfaces;
using Injhinuity.Backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Injhinuity.Backend.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddFirestore(this IServiceCollection services, IBackendConfig config)
        {
            services.AddSingleton(new FirestoreDbBuilder
            {
                ProjectId = config.Firestore.ProjectId,
                EmulatorDetection = config.Firestore.IsEmulated ? EmulatorDetection.EmulatorOnly : EmulatorDetection.ProductionOnly
            }.Build());

            return services;
        }

        public static IServiceCollection AddLogging(this IServiceCollection services, IBackendConfig config) =>
            services.AddLogging(opt => opt.AddConsole())
                .Configure<LoggerFilterOptions>(opt => opt.MinLevel = config.Logging.LogLevel);

        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddTransient<IGuildRepository, GuildFirestoreRepository>()
                .AddTransient<ICommandRepository, CommandFirestoreRepository>();

        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddTransient<IFirestoreProvider, FirestoreProvider>()
                .AddTransient<IGuildService, GuildService>()
                .AddTransient<ICommandService, CommandService>();
    }
}

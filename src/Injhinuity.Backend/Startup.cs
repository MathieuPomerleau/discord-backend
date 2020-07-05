using Injhinuity.Backend.Core.Configuration;
using Injhinuity.Backend.Core.Configuration.Options;
using Injhinuity.Backend.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Injhinuity.Backend
{
    public class Startup
    {
        private readonly IBackendConfigMapper _configMapper = new BackendConfigMapper();

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var options = Configuration.GetSection(IBackendOptions.SectionName).Get<BackendOptions>();
            var config = _configMapper.MapFromNullableOptions(options);

            services.AddMvc(o => o.EnableEndpointRouting = false);
            services.AddControllers();

            services
                .AddLogging(config)
                .AddFirestore(config)
                .AddRepositories()
                .AddServices();

            services.AddSingleton(config);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}

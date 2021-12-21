using Finanzuebersicht.Backend.Core.API.APIConfiguration;
using Finanzuebersicht.Backend.Core.API.Contexts;
using Finanzuebersicht.Backend.Core.API.Contexts.Pagination;
using Finanzuebersicht.Backend.Core.API.Middlewares;
using Finanzuebersicht.Backend.Core.API.Security.Authentication;
using Finanzuebersicht.Backend.Core.Contract;
using Finanzuebersicht.Backend.Core.Contract.Contexts;
using Finanzuebersicht.Backend.Core.Contract.Contexts.Pagination;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.SessionManagement.Sessions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Finanzuebersicht.Backend.Core.API
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Logic.DependencyProvider.Startup(services, this.configuration);
            Persistence.DependencyProvider.Startup(services);

            services.AddScoped<ISessionContext, SessionContext>();
            services.AddScoped<IPaginationContext, PaginationContext>();

            BadRequestLogging.Configure(services);

            CrossOriginResourceSharing.Configure(services, this.configuration);

            services.AddAuthentication(TokenAuthentication.Scheme).AddTokenAuthentication<ISessionsCrudLogic>();

            services.AddControllers();

            SwaggerSetup.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SwaggerSetup.Configure(app);

            app.UseCors();
            app.UseRouting();
            app.UseMiddleware<ExceptionLoggingMiddleware>();
            app.UseMiddleware<PaginationExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
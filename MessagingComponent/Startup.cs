using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RealmDigital.Data;
using RealmDigital.Data.Interfaces;
using RealmDigital.Logic.Core;
using RealmDigital.Logic.Interfaces;
using RealmDigital.MessagingService.Api.Middleware;
using RealmDigitial.Libraries.Messaging;
using System.Text.Json;

namespace MessagingComponent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRealmDigitalRepository, RealmDigitalRepository>();
            services.AddScoped<IEmployeeMessagesLogic, EmployeeMessagesLogic>();
            services.AddControllers().AddJsonOptions( options => {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
            services.AddMessaging();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MessagingComponent", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessagingComponent v1"));
            }

            app.UseHttpsRedirection();

            app.UseErrorHandlingMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

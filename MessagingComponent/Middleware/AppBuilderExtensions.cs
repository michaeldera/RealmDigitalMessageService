using Microsoft.AspNetCore.Builder;

namespace RealmDigital.MessagingService.Api.Middleware
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandler>();
            return app;
        }
    }
}

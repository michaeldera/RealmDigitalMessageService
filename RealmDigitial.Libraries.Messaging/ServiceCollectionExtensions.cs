using Microsoft.Extensions.DependencyInjection;
using RealmDigitial.Libraries.Messaging.Core;
using RealmDigitial.Libraries.Messaging.Interfaces;

namespace RealmDigitial.Libraries.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessagingHandler>(new MessagingHandler());
            return services;
        }
    }
}

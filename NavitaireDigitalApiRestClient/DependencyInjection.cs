using Microsoft.Extensions.DependencyInjection;

namespace NavitaireDigitalApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNavitaireDigitalApiRestClient(this IServiceCollection services)
        {
            services.AddSingleton<NavitaireDigitalApiRestClient>();

            return services;
        }
    }
}
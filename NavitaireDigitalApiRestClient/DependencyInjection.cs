using Microsoft.Extensions.DependencyInjection;

namespace NavitaireDigitalApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNavitaireDigitalApiRestClient(this IServiceCollection services, string baseUrl)
        {
            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl[..^1];
            }

            Config.BaseUrl = baseUrl;

            services.AddSingleton<NavitaireDigitalApiRestClient>();

            return services;
        }
    }
}
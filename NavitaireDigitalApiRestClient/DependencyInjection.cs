using Microsoft.Extensions.DependencyInjection;

namespace NavitaireDigitalApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNavitaireDigitalApiRestClient(this IServiceCollection services, string baseUrl, string httpClientName = "", Func<Task<string>>? getNavitairetokenAsyncDelegate = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl[..^1];
            }

            Config.BaseUrl = baseUrl;

            Config.HttpClientName = httpClientName;

            Config.GetNavitairetokenAsyncDelegate = getNavitairetokenAsyncDelegate;

            services.AddSingleton<NavitaireDigitalApiRestClient>();

            return services;
        }
    }
}
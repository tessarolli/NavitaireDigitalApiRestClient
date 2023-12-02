using System.Net.Http.Headers;
using System.Text;

namespace NavitaireDigitalApi;

public partial class NavitaireDigitalApiRestClient
{
    public NavitaireDigitalApiRestClient(IHttpClientFactory httpClientFactory)
    {
        if (httpClientFactory is null)
        {
            throw new ArgumentNullException(nameof(httpClientFactory));
        }

        if (string.IsNullOrEmpty(Config.BaseUrl))
        {
            throw new System.Exception("You have to initialize with builder.Services.AddNavitaireDigitalApiRestClient()");
        }

        _httpClient = httpClientFactory.CreateClient(Config.HttpClientName);

        _settings = new Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings, true);

        BaseUrl = Config.BaseUrl;
    }

    public Task PrepareRequestAsync(HttpClient client, HttpRequestMessage requestMessage, StringBuilder stringBuilder, CancellationToken ct)
    {
        return Task.CompletedTask;
    }

    public async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage requestMessage, string url, CancellationToken ct)
    {
        if (Config.GetNavitairetokenAsyncDelegate is null)
        {
            throw new InvalidOperationException("You have to declare the getNavitaireTokenDelegate with builder.Services.AddNavitaireDigitalApiRestClient() before using this code.");
        }

        var token = await Config.GetNavitairetokenAsyncDelegate.Invoke();
        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidOperationException("The GetNavitaireTokenDelegate did not returned a valid value.");
        }

        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public Task ProcessResponseAsync(HttpClient client, HttpResponseMessage requestMessage, CancellationToken ct)
    {
        return Task.CompletedTask;
    }

    public Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken ct)
    {
        return Task.FromResult(new HttpRequestMessage(HttpMethod.Get, Config.BaseUrl));
    }

}

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
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await TokenService.GetTokenAsync(client));
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

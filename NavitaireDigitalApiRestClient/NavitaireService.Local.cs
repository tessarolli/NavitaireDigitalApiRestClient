using System.Net.Http.Headers;
using System.Text;

namespace NavitaireDigitalApi;

public partial class NavitaireDigitalApiRestClient
{
    public NavitaireDigitalApiRestClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _settings = new Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings, true);
     
        if (string.IsNullOrEmpty(Config.BaseUrl))
        {
            throw new System.Exception("You have to initialize with builder.Services.AddNavitaireDigitalApiRestClient()");
        }

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

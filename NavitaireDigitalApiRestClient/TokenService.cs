using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NavitaireDigitalApi;

/// <summary>
/// Provides services for Autonomously Getting tokens for accessing navitaire's API
/// </summary>
internal static class TokenService
{
    /// <summary>
    /// The Last Request Time in UTC.
    /// If it's Greater than 15 minutes, Fetches a new Token from Navitaire's API.
    /// </summary>
    private static DateTime _lastRequestTimeUTC = DateTime.UtcNow.AddMinutes(-30);

    /// <summary>
    /// The Last Token returned from Navitaire's API.
    /// </summary>
    private static string _token = string.Empty;

    /// <summary>
    /// Idle Timeout in minutes to invalidate the current token.
    /// </summary>
    private static int _idleTimeoutInMinutes = 15;

    /// <summary>
    /// Fetches the Bearer Token for Navitaire's API.
    /// </summary>
    /// <returns>The Bearer Token, or Empty String if failed.</returns>
    public static async Task<string?> GetTokenAsync(HttpClient client)
    {
        if (ShouldUseCachedToken())
        {
            _lastRequestTimeUTC = DateTime.UtcNow;

            return _token;
        }

        var stringContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var message = new HttpRequestMessage(HttpMethod.Post, $"{client.BaseAddress}/f9/dotrez/api/nsk/v2/token") { Content = stringContent };

        // Anonymous call to bypass authentication
        message.Options.Set(new HttpRequestOptionsKey<bool>("Anonymous"), true);

        var response = await client.SendAsync(message);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var jsonObjectResponse = JsonSerializer.Deserialize<JsonObject>(content);

            if (jsonObjectResponse is not null)
            {
                _token = (string)jsonObjectResponse["data"]!["token"]!;

                _idleTimeoutInMinutes = (int)jsonObjectResponse["data"]!["idleTimeoutInMinutes"]!;

                _lastRequestTimeUTC = DateTime.UtcNow;

                return _token;
            }
        }

        return string.Empty;
    }

    private static bool ShouldUseCachedToken() =>
        _lastRequestTimeUTC.AddMinutes(_idleTimeoutInMinutes) > DateTime.UtcNow &&
            !string.IsNullOrEmpty(_token);
}
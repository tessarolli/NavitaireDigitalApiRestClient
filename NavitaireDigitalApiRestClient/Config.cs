namespace NavitaireDigitalApi;

/// <summary>
/// Internal class for storing in process configuration values.
/// </summary>
internal static class Config
{
    /// <summary>
    /// Gets or Sets the Navitaire's Digital API URI.
    /// </summary>
    public static string BaseUrl { get; set; } = "";

    /// <summary>
    /// Gets or Sets the Http Client Name to be used on this library.
    /// </summary>
    public static string HttpClientName { get; set; } = "";

}

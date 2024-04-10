namespace  RagWithSDKs;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Reads the application settings from the configuration files.
/// </summary>
public class AppSettingsReader
{
    private readonly IConfigurationRoot _configurationRoot;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppSettingsReader"/> class.
    /// </summary>
    public AppSettingsReader()
    {
        var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();
        _configurationRoot = builder.Build();
    }

    /// <summary>
    /// Reads a specific section from the application settings.
    /// </summary>
    /// <typeparam name="T">The type of the settings object for the section.</typeparam>
    /// <param name="sectionName">The name of the section.</param>
    /// <returns>The settings object for the section.</returns>
    public T ReadSection<T>(string sectionName) => _configurationRoot.GetSection(sectionName).Get<T>();
}
using Demo_APP.Helpers;
using Demo_APP.Interfaces;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class FirebaseConfigHelperService: IFirebaseConfigHelperService
{
    private readonly string _configFilePath;
    private readonly IConfiguration configuration;

    public FirebaseConfigHelperService(IConfiguration configuration)
    {
        this.configuration = configuration;
        _configFilePath = configuration["FirebaseApp:FirebaseConfigFilePath"];
    }

    // Read the JSON file into the C# object
    public async Task<FirebaseAppConfigRoot?> ReadConfigAsync()
    {
        if (!File.Exists(_configFilePath))
        {
            throw new FileNotFoundException($"The configuration file {_configFilePath} was not found.");
        }

        var jsonString = await File.ReadAllTextAsync(_configFilePath);
        var config = JsonSerializer.Deserialize<FirebaseAppConfigRoot>(jsonString);
        return config;
    }

    // Update the DeviceRegistrationToken and save the changes
    public async Task UpdateDeviceRegistrationTokenAsync(string newToken)
    {
        var config = await ReadConfigAsync();

        if (config != null && config.FirebaseApp != null)
        {
            config.FirebaseApp.DeviceRegistrationToken = newToken;

            // Save the updated config
            await WriteConfigAsync(config);
        }
    }

    // Write the updated object back to the JSON file
    public async Task WriteConfigAsync(FirebaseAppConfigRoot config)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(config, options);

        await File.WriteAllTextAsync(_configFilePath, jsonString);
    }
}

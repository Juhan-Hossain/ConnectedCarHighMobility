using Demo_APP.Dtos;
using Demo_APP.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Demo_APP.Services
{
    public class UpdateAppSettingsService : IUpdateAppSettingsService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public UpdateAppSettingsService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        public void UpdateAppSetting(AppSettingsUpdateDto dto)
        {
            // Define the path to the appsettings.json file
            var appSettingsPath = Path.Combine(_env.ContentRootPath, "appsettings.json");

            // Load the appsettings.json file
            var json = System.IO.File.ReadAllText(appSettingsPath);
            var jsonDocument = JsonDocument.Parse(json);
            var jsonObject = JsonDocumentToObject(jsonDocument);

            // Navigate to the specified section and update the key value
            if (jsonObject.ContainsKey(dto.Section))
            {
                var section = jsonObject[dto.Section] as Dictionary<string, object>;
                if (section != null && section.ContainsKey(dto.Key))
                {
                    section[dto.Key] = dto.Value;
                }
            }

            // Save the updated appsettings.json file
            var updatedJson = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(appSettingsPath, updatedJson);

        }

        private Dictionary<string, object> JsonDocumentToObject(JsonDocument jsonDocument)
        {
            // Convert JsonDocument to Dictionary for easy manipulation
            return JsonSerializer.Deserialize<Dictionary<string, object>>(jsonDocument.RootElement.GetRawText());
        }
    }
}

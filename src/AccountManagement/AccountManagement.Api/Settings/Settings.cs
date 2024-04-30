namespace AccountManagement.Api.Settings
{
    public class Settings : ISettings
    {
        private readonly IConfiguration _config;

        public Settings(IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(config, nameof(config));

            _config = config;
        }

        public string GetStringSetting(string key)
        {
            return GetSettingValue(key);
        }

        private string GetSettingValue(string key)
        {
            return _config[key] ?? throw new ArgumentException($"Setting [{key}] does not exist in appsettings.json");
        }
    }
}

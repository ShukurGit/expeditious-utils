
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Expeditious.Common
{

    public static class ConfigFileHelper
    {
        public static IConfigurationRoot LoadConfiguration(
            string? basePath,
            string jsonFileName)
        {
            if (string.IsNullOrWhiteSpace(jsonFileName))
                throw new ArgumentException("JSON configuration file name cannot be empty.", nameof(jsonFileName));

            string actualBasePath = string.IsNullOrWhiteSpace(basePath)
                ? Directory.GetCurrentDirectory()
                : basePath;

            if (!Directory.Exists(actualBasePath))
                throw new DirectoryNotFoundException($"Configuration base path not found: '{actualBasePath}'.");

            string fullPath = Path.Combine(actualBasePath, jsonFileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Configuration file not found: '{fullPath}'.", fullPath);

            return new ConfigurationBuilder()
                .SetBasePath(actualBasePath)
                .AddJsonFile(jsonFileName, optional: false, reloadOnChange: false)
                .Build();
        }

        public static string GetConnectionString(
            string? basePath,
            string jsonFileName,
            string connectionStringName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
                throw new ArgumentException("Connection string name cannot be empty.", nameof(connectionStringName));

            IConfigurationRoot configuration = LoadConfiguration(basePath, jsonFileName);

            string? value = configuration.GetConnectionString(connectionStringName);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(
                    $"Connection string '{connectionStringName}' was not found in configuration file '{jsonFileName}'.");
            }

            return value;
        }

        public static Dictionary<string, string> GetConnectionStrings(
            string? basePath,
            string jsonFileName,
            IEnumerable<string> names)
        {
            if (names is null)
                throw new ArgumentNullException(nameof(names));

            IConfigurationRoot configuration = LoadConfiguration(basePath, jsonFileName);

            var result = new Dictionary<string, string>();

            foreach (string name in names)
            {
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                string? value = configuration.GetConnectionString(name);

                result[name] = value ?? string.Empty;
            }

            return result;
        }

        public static Dictionary<string, string?> GetAllValues(
            string? basePath,
            string jsonFileName)
        {
            IConfigurationRoot configuration = LoadConfiguration(basePath, jsonFileName);

            return configuration
                .AsEnumerable()
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static string? GetValue(
            string? basePath,
            string jsonFileName,
            string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Configuration key cannot be empty.", nameof(key));

            IConfigurationRoot configuration = LoadConfiguration(basePath, jsonFileName);

            return configuration[key];
        }

        public static string GetRequiredValue(
            string? basePath,
            string jsonFileName,
            string key)
        {
            string? value = GetValue(basePath, jsonFileName, key);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(
                    $"Configuration value '{key}' was not found in configuration file '{jsonFileName}'.");
            }

            return value;
        }
    }
}


/*
 
usage


 {
  "ConnectionStrings": {
    "Default": "Data Source=app.db",
    "Postgres": "Host=localhost;Database=test;Username=postgres;Password=123"
  },
  "App": {
    "Name": "RiskAddressSearch",
    "Version": "1.0.0"
  }
}




string connStr = ConfigFileHelper.GetConnectionString(
    basePath: null,
    jsonFileName: "appsettings.json",
    connectionStringName: "Default");



string appName = ConfigFileHelper.GetRequiredValue(
    basePath: null,
    jsonFileName: "appsettings.json",
    key: "App:Name");
 
 */
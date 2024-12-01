
using Microsoft.Extensions.Configuration;
using AssurityAPITests.Log;


namespace AssurityAPITests
{
    public static class AppConfig
    {
        private static readonly IConfiguration builder;
        public static string environment;

        static AppConfig()
        {
            // Read the "RuntimeEnvironment" value from profile.json
            environment = getCurrentEnvironment();
            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"config/appsettings-{environment}.json")              
              .Build();
        }
    
        public static string GetConfigValue(string keyName)
        {
            var configValue = builder.GetSection("ApiSettings")[keyName];
            if (configValue == null || configValue == string.Empty || configValue == "")
            {
                throw new KeyNotFoundException("'key Not found");
            }
            else { return configValue; }
            
        }

        public static string getCurrentEnvironment()
        {
            // Load the base profile.json first
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config/profile.json")
                .Build();

            // Read the "RuntimeEnvironment" value from profile.json
            environment = config["RuntimeEnvironment"] ?? "test";

            return environment;
        }


    }

}



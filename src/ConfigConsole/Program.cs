using System;
using Microsoft.Extensions.Configuration;

namespace ConfigConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettings = configuration.Get<AppSettings>();

            if (appSettings.NotOverridden == null)
            {
                Console.WriteLine("The NotOverridden config element is not present");
            }
            else
            {
                Console.WriteLine("The NotOverridden config element is present:");
                Console.WriteLine($"\tIntValue={appSettings.NotOverridden.IntValue}");
                Console.WriteLine($"\tStringValue={appSettings.NotOverridden.StringValue}");
            }
        }
    }

    public class AppSettings
    {
        public NotOverriddenConfig NotOverridden { get; set; }
    }

    public class NotOverriddenConfig
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
    }
}

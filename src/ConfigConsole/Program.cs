using System;
using Microsoft.Extensions.Configuration;

namespace ConfigConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.override.json", true)
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

            if (appSettings.PartOverridden == null)
            {
                Console.WriteLine("The PartOverridden config element is not present");
            }
            else
            {
                Console.WriteLine("The PartOverridden config element is present:");
                Console.WriteLine($"\tIntValue={appSettings.PartOverridden.IntValue}");
                Console.WriteLine($"\tStringValue={appSettings.PartOverridden.StringValue}");
            }
        }
    }

    public class AppSettings
    {
        public SomeConfig NotOverridden { get; set; }
        public SomeConfig PartOverridden { get; set; }
    }

    public class SomeConfig
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
    }
}

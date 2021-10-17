using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TssT.TelegramBot.Services;

namespace TssT.TelegramBot
{
    class Program
    {
        static async Task Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
            
            var service = new TelegramBotService(config["TelegramBotToken"], config["ApiUrl"]);

            service.OnError += message =>
            {
                Console.WriteLine($"{DateTime.Now} {message}");
            };

            service.OnMessage += message =>
            {
                Console.WriteLine($"{DateTime.Now} {message}");
            };
            
            await service.StartAsync();

            Console.ReadKey();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"{DateTime.UtcNow} {e.ExceptionObject}");
        }
    }
}

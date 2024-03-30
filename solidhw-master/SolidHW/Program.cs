using Microsoft.Extensions.Configuration;
using System;

namespace OtusHW2
{
    internal class Program
    {
        static void Main()
        {
            IConfigurationRoot config = LoadConfiguration();

            int minNum = int.Parse(config["GameSettings:MinNum"]);
            int maxNum = int.Parse(config["GameSettings:MaxNum"]);
            int tryCount = int.Parse(config["GameSettings:TryCount"]);

            IGame game = new Game(minNum, maxNum, tryCount);
            game.StartGame();
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            return configBuilder.Build();
        }
    }
}
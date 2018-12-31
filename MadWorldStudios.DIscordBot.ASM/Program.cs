using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace MadWorldStudios.DIscordBot.ASM
{
    class Program
    {
        private DiscordSocketClient _client;

        public static void Main(string[] args)
                    => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var discordToken = GetDiscordTokenFromAppSettings();

            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageReceived;            
            
            await _client.LoginAsync(TokenType.Bot, discordToken);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private string GetDiscordTokenFromAppSettings()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();


            return configuration["discord_token"];
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content.StartsWith("!ark "))
            {
                await Log(message.Content);
                var router = new MessageRouter(message);

                var result = await router.GetResponse();                               

                await Log(result);
                await message.Channel.SendMessageAsync(result);
            }
        }

        private Task Log(string msg)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] : {msg}");
            return Task.CompletedTask;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] : {msg.ToString()}");
            return Task.CompletedTask;
        }
    }       
}

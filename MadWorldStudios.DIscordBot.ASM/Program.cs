using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace MadWorldStudios.DIscordBot.ASM
{
    class Program
    {
        private DiscordSocketClient _client;

        public static void Main(string[] args)
                    => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageReceived;

            
            string token = ""; // Remember to keep this private!
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content.StartsWith("!asm "))
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

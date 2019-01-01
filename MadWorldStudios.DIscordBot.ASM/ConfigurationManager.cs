using System.Collections.Generic;
using System.IO;
using System.Linq;
using MadWorldStudios.DIscordBot.ASM.Models;
using MadWorldStudios.DIscordBot.ASM.Exceptions;
using Microsoft.Extensions.Configuration;

namespace MadWorldStudios.DIscordBot.ASM
{
    public class ConfigurationManager
    {
        private IConfigurationRoot _configuration;
        private List<Server> _servers;

        public string DiscordToken
        {   get
            {
                return _configuration["discord_token"];
            }
        }

        public ConfigurationManager()
        {
            _servers = new List<Server>();
            InitializeConfiguration();
        }

        private void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();

            InitializeServers();
        }

        private void InitializeServers()
        {
            var servers = _configuration.GetSection("Servers")
                                   .GetChildren();

            foreach (var server in servers)
            {
                var s = new Server();
                s.Name = server["Name"];
                s.ShortName = server["ShortName"];
                s.StartCommand = server["StartCommand"];

                if (s.Name == null || s.StartCommand == null || s.ShortName == null) throw new InvalidServerSettingsException();

                _servers.Add(s);
            }
        }

        public Server FindServerByName(string serverName)
        {
            var server = _servers.FirstOrDefault(s => s.ShortName == serverName);

            if (server == null) throw new ServerNotFoundException();

            return server;
        }
    }
}

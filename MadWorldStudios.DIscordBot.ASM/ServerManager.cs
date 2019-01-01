using System;
using System.Threading.Tasks;
using MadWorldStudios.DIscordBot.ASM.Models;
using System.Diagnostics;

namespace MadWorldStudios.DIscordBot.ASM
{
    public class ServerManager
    {
        private ConfigurationManager _configurationManager;

        public ServerManager(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public bool StartServer(string serverName)
        {
            Server server = _configurationManager.FindServerByName(serverName);

            try
            { 
                var process = new Process();
                process.StartInfo.FileName = server.StartCommand;
                process.Start();
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> StopServer(string serverName)
        {
            throw new NotImplementedException();
        }            
    }
}

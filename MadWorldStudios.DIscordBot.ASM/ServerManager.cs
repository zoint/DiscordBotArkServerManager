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

        public async Task<bool> StartServer(string serverName)
        {
            Server server = _configurationManager.FindServerByName(serverName);

            //TODO: Add better statuses than true / false...
            if (await IsServerRunning(server.Name)) return true;

            try
            { 
                await Task.Run( () => { 
                        var process = new Process();
                        process.StartInfo.FileName = server.StartCommand;
                        process.Start();
                });
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> StopServer(string serverName)
        {
            Server server = _configurationManager.FindServerByName(serverName);

            if (await IsServerRunning(server.Name))
            {
                var process = await GetProcessByForServerName(server.Name);
                process.Kill();

                return true;
            }

            return false;
        }

        private async Task<bool> IsServerRunning(string serverName)
        {
            var process = await GetProcessByForServerName(serverName);

            if (process == null) return false;

            return true;
        }

        private async Task<Process> GetProcessByForServerName(string serverName)
        {
            var process = await Task.Run(() =>
            {
                Process[] processes = Process.GetProcessesByName("ShooterGameServer");
                foreach (Process p in processes)
                {
                    if (p.MainWindowTitle == serverName)
                    {
                        return p;
                    }
                }

                return null;
            });

            return process;
        }
    }
}

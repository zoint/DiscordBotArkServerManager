using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace MadWorldStudios.DIscordBot.ASM
{
    public class MessageRouter
    {
        private SocketMessage _message;
        private ServerManager _manager;
        
        public MessageRouter(SocketMessage message)
        {
            _message = message;
            _manager = new ServerManager();
        }

        public async Task<string> GetResponse()
        {
            var messageSegments = _message.Content.Split(' ');
            var response = String.Empty;
            bool success = false;

            if (messageSegments.Length != 3)
            {
                //there is no valid command, so prompt the user to ask for help
                return GetHelpPrompt();                
            }

            //TODO: need to make this more elegant over the long term
            switch (messageSegments[1])
            {
                case "start":                    
                    success = await _manager.StartServer(messageSegments[2]);
                    break;
                case "stop":
                    success = await _manager.StopServer(messageSegments[2]);
                    break;
                default:
                    return GetHelpPrompt();
            }

            //TODO: will likely need to refactor this if commands get more complex
            if(success == false)
            {
                return $"No valid server found with the name {messageSegments[2]}.";
            }

            //todo: this is just a place holder
            return "Processing your command.";
        }

        private string GetHelpPrompt()
        {
            var author = _message.Author.Username;

            return string.Format($"I'm sorry {author}, \"{_message.Content}\" is not a valid command." +
                                                    $"Please type \"!asm help\" for a list of valid commands. ");
        }
    }
}

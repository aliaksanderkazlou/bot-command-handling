using System;

namespace Bot.CommandHandling.Core.Attributes
{
    public class CommandHandlerAttribute : Attribute
    {
        public string Command { get; }
        
        public string[] StatusesToHandle { get; }
        
        public CommandHandlerAttribute(string command, string[] statusesToHandle = null)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            StatusesToHandle = statusesToHandle ?? new string[0];
        }
    }
}
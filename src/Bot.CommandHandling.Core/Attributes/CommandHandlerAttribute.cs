using System;

namespace Bot.CommandHandling.Core.Attributes
{
    public class CommandHandlerAttribute : Attribute
    {
        public string Command { get; }
        
        public string[] StatusesToHandle { get; }
        
        public CommandHandlerAttribute(string command, string[] statusesToHandle = null)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("Parameter should not be null or whitespace", nameof(command));
            }

            Command = command;
            StatusesToHandle = statusesToHandle ?? new string[0];
        }
    }
}
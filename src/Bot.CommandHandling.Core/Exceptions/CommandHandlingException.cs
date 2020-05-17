using System;

namespace Bot.CommandHandling.Core.Exceptions
{
    [Serializable]
    public class CommandHandlingException : Exception
    {
        public CommandHandlingExceptionType Type { get; }
        
        public CommandHandlingException(string message) : base(message) {}
        
        public CommandHandlingException(string message, Exception innerException) : base(message, innerException) {}

        public CommandHandlingException(CommandHandlingExceptionType type)
        {
            Type = type;
        }

        public CommandHandlingException(string message, CommandHandlingExceptionType type) : base(message)
        {
            Type = type;
        }
    }
}
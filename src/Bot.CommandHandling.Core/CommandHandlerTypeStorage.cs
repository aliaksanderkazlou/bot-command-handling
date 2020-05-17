using System;
using System.Collections.Generic;
using System.Linq;
using Bot.CommandHandling.Core.Attributes;
using Bot.CommandHandling.Core.Exceptions;

namespace Bot.CommandHandling.Core
{
    internal static class CommandHandlerTypeStorage<T, TResult>
    {
        private static Dictionary<(string command, string[] statuses), Type> _dictionary = new Dictionary<(string command, string[] statuses), Type>();

        public static void Initialize()
        {
            var type = typeof(ICommandHandler<T, TResult>);
            var handlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && p.GetCustomAttributes(typeof(CommandHandlerAttribute), true).Any())
                .ToArray();

            foreach (var handler in handlers)
            {
                var attribute =
                    handler.GetCustomAttributes(typeof(CommandHandlerAttribute), true).First() as
                        CommandHandlerAttribute;

                if (_dictionary.Keys.SingleOrDefault(k => k.command.Equals(attribute.Command)).command != null)
                {
                    throw new CommandHandlingException($"Implementation of ICommandHandler {handler.FullName} duplicates the command {attribute.Command}");
                }
                
                _dictionary.Add((attribute.Command, attribute.StatusesToHandle), handler);
            }
        }

        public static Type GetByCommand(string command)
        {
            return GetByKey(_dictionary.Keys.SingleOrDefault(k => k.command.Equals(command)));
        }

        public static Type GetByStatus(string status)
        {
            return GetByKey(_dictionary.Keys.SingleOrDefault(k => k.statuses.Contains(status)));
        }

        private static Type GetByKey((string command, string[] statuses) key)
        {
            if (key.command is null)
            {
                throw new CommandHandlingException(CommandHandlingExceptionType.HandlerNotFound);
            }
            
            return _dictionary[key];
        }

        public static IEnumerable<Type> GetAll()
        {
            return _dictionary.Values;
        }
    }
}
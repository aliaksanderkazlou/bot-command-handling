using System;
using System.Collections.Generic;
using System.Linq;
using Bot.CommandHandling.Core.Attributes;
using Bot.CommandHandling.Core.Exceptions;

namespace Bot.CommandHandling.Core
{
    internal static class CommandHandlerTypeStorage
    {
        private static Dictionary<(string command, string[] statuses), Type> _dictionary;

        public static void Initialize()
        {
            var type = typeof(ICommandHandler);
            var handlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && p.CustomAttributes.Any())
                .ToArray();

            _dictionary = handlers.ToDictionary(handler =>
            {
                var attribute = handler.GetCustomAttributes(true).Single() as CommandHandlerAttribute;

                return (attribute?.Command, attribute?.StatusesToHandle);
            }, handler => handler);
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
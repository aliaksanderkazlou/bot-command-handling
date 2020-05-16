using Bot.CommandHandling.Core.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.CommandHandling.Core.Extensions
{
    public static class CommandHandlingExtensions
    {
        public static IServiceCollection AddCommandHandlingDependencies(this IServiceCollection collection)
        {
            CommandHandlerTypeStorage.Initialize();

            var types = CommandHandlerTypeStorage.GetAll();

            foreach (var type in types)
            {
                collection.AddTransient(type);
            }
            
            collection.AddTransient<ICommandHandlerFactory, CommandHandlerFactory>();

            return collection;
        }
    }
}
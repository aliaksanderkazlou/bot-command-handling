using Bot.CommandHandling.Core.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.CommandHandling.Core.Extensions
{
    public static class CommandHandlingExtensions
    {
        public static IServiceCollection AddCommandHandlingDependencies<T, TResult>(this IServiceCollection collection)
        {
            CommandHandlerTypeStorage<T, TResult>.Initialize();

            var types = CommandHandlerTypeStorage<T, TResult>.GetAll();

            foreach (var type in types)
            {
                collection.AddTransient(type);
            }
            
            collection.AddTransient<ICommandHandlerFactory<T, TResult>, CommandHandlerFactory<T, TResult>>();

            return collection;
        }
    }
}
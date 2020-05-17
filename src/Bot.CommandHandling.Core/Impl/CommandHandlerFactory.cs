using System;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.CommandHandling.Core.Impl
{
    public class CommandHandlerFactory<T, TResult> : ICommandHandlerFactory<T, TResult>
    {
        private readonly IServiceProvider _serviceProvider;
        
        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        
        public ICommandHandler<T, TResult> GetByCommand(string command)
        {
            return _serviceProvider.GetRequiredService(CommandHandlerTypeStorage<T, TResult>.GetByCommand(command)) as ICommandHandler<T, TResult>;
        }

        public IContextCommandHandler<T, TResult> GetByContextStatus(string status)
        {
            return _serviceProvider.GetRequiredService(CommandHandlerTypeStorage<T, TResult>.GetByStatus(status)) as
                IContextCommandHandler<T, TResult>;
        }
    }
}
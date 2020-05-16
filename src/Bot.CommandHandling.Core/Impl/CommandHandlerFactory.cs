using System;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.CommandHandling.Core.Impl
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        
        public ICommandHandler GetByCommand(string command)
        {
            return _serviceProvider.GetRequiredService(CommandHandlerTypeStorage.GetByCommand(command)) as ICommandHandler;
        }

        public IContextCommandHandler GetByContextStatus(string status)
        {
            return _serviceProvider.GetRequiredService(CommandHandlerTypeStorage.GetByStatus(status)) as
                IContextCommandHandler;
        }
    }
}
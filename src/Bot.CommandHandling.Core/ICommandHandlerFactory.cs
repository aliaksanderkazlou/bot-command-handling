namespace Bot.CommandHandling.Core
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler GetByCommand(string command);
        IContextCommandHandler GetByContextStatus(string status);
    }
}
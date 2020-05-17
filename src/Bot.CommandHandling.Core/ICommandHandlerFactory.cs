namespace Bot.CommandHandling.Core
{
    public interface ICommandHandlerFactory<T, TResult>
    {
        ICommandHandler<T, TResult> GetByCommand(string command);
        IContextCommandHandler<T, TResult> GetByContextStatus(string status);
    }
}
using System.Threading.Tasks;

namespace Bot.CommandHandling.Core
{
    public interface IContextCommandHandler : ICommandHandler
    {
        Task<TResult> HandleContextAsync<T, TResult>(T message);
    }
}
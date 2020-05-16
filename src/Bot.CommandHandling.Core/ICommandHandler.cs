using System.Threading.Tasks;

namespace Bot.CommandHandling.Core
{
    public interface ICommandHandler
    {
        Task<TResult> HandleAsync<T, TResult>(T message);
    }
}
using System.Threading.Tasks;

namespace Bot.CommandHandling.Core
{
    public interface ICommandHandler<T, TResult>
    {
        Task<TResult> HandleAsync(T message);
    }
}
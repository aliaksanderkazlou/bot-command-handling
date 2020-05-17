using System.Threading.Tasks;

namespace Bot.CommandHandling.Core
{
    public interface IContextCommandHandler<T, TResult> : ICommandHandler<T, TResult>
    {
        Task<TResult> HandleContextAsync(T message);
    }
}
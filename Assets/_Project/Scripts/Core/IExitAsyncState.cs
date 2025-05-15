using System.Threading.Tasks;

namespace Project.Core
{
    public interface IExitAsyncState : IState
    {
        Task ExitAsync();
    }
}

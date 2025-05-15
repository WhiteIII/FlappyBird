using System.Threading.Tasks;

namespace Project.Core
{
    public interface IEnterAsyncState : IState 
    {
        Task EnterAsync();
    }
}

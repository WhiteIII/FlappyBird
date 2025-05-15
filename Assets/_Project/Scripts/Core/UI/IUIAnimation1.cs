using System;
using System.Threading.Tasks;

namespace Project.Core.UI
{
    public interface IUIAnimation<TData> : IDisposable
    {
        Task PlayAnimationAsync(TData data);
    }
}

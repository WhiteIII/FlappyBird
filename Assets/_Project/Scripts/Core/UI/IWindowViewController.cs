using System.Threading.Tasks;

namespace Project.Core.UI
{
    public interface IWindowViewController
    {
        Task PlayShowAnimationAsync();
        Task PlayHideAnimationAsync();

    }
}

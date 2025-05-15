using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Core.UI
{
    public class BaseUIControllerWithAlphaAnimation : IDisposable, IWindowViewController
    {
        private readonly IUIAnimation<AlphaAnimationData> _animation;
        private readonly GameObject _popupGameObject;

        public BaseUIControllerWithAlphaAnimation(
            IUIAnimation<AlphaAnimationData> animation,
            GameObject popupGameObject)
        {
            _animation = animation;
            _popupGameObject = popupGameObject;
        }

        public void Dispose() =>
            _animation.Dispose();

        public async Task PlayHideAnimationAsync()
        {
            await _animation.PlayAnimationAsync(new AlphaAnimationData { From = 1f, To = 0f });
            _popupGameObject.SetActive(false);
        }

        public async Task PlayShowAnimationAsync()
        {
            _popupGameObject.SetActive(true);
            await _animation.PlayAnimationAsync(new AlphaAnimationData { From = 0f, To = 1f });
        }
    }
}

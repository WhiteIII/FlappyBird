using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Core.UI
{
    public class BaseWindowViewController : IDisposable, IWindowViewController
    {
        private readonly IUIAnimation<BaseAnimationData> _animation;
        private readonly GameObject _windowGameObject;
        private readonly Vector2 _positionFrom;
        private readonly Vector2 _positionTo;

        public BaseWindowViewController(
            IUIAnimation<BaseAnimationData> animation,
            Vector2 animationPositionFrom,
            Vector2 animationPositionTo,
            GameObject windowGameObject)
        {
            _animation = animation;
            _windowGameObject = windowGameObject;
            _positionFrom = animationPositionFrom;
            _positionTo = animationPositionTo;
        }

        public void Dispose() =>
            _animation.Dispose();

        public async Task PlayHideAnimationAsync()
        {
            await _animation.PlayAnimationAsync(new BaseAnimationData {
                PositionFrom = _positionTo,
                PositionTo = _positionFrom,
                AlphaFrom = 1f,
                AlphaTo = 0f});
            _windowGameObject.SetActive(false);
        }

        public async Task PlayShowAnimationAsync()
        {
            _windowGameObject.SetActive(true);
            await _animation.PlayAnimationAsync(new BaseAnimationData
            {
                PositionFrom = _positionFrom,
                PositionTo = _positionTo,
                AlphaFrom = 0f,
                AlphaTo = 1f
            });
        }
    }
}

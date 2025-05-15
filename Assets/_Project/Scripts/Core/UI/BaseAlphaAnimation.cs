using System;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Time;

namespace Project.Core.UI
{
    public class BaseAlphaAnimation : IUIAnimation<AlphaAnimationData>
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly float _duration;

        private bool _animationPlayingIsPossible = true;

        public BaseAlphaAnimation(
            CanvasGroup canvasGroup, float duration)
        {
            _canvasGroup = canvasGroup;
            _duration = duration;
        }

        public void Dispose() =>
            _animationPlayingIsPossible = false;

        public Task PlayAnimationAsync(AlphaAnimationData data) =>
            AlphaAnimation(data.From, data.To);

        private async Task AlphaAnimation(float from, float to)
        {
            float time = 0f;
            _canvasGroup.alpha = from;
            while (_duration >= time && _animationPlayingIsPossible)
            {
                time += deltaTime;
                float t = Mathf.Clamp01(time / _duration);
                _canvasGroup.alpha = Mathf.Lerp(from, to, t);
                await Task.Delay(Convert.ToInt32(deltaTime * 1000));
            }
            _canvasGroup.alpha = to;
        }
    }

    public struct AlphaAnimationData
    {
        public float From;
        public float To;
    }
}

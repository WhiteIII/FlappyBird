using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Time;

namespace Project.Core.UI
{
    public class BaseWindowAnimation : IUIAnimation<BaseAnimationData>
    {
        private readonly RectTransform _windowRectTransform;
        private readonly CanvasGroup _canvasGroup;
        private readonly float _duration;
        private readonly IUIAnimation<AlphaAnimationData> _alphaAnimation;

        private bool _animationPlayingIsPossible = true;

        public BaseWindowAnimation(
            RectTransform windowRectTransform,
            CanvasGroup canvasGroup,
            float duration)
        {
            _windowRectTransform = windowRectTransform;
            _canvasGroup = canvasGroup;
            _duration = duration;
            _alphaAnimation = new BaseAlphaAnimation(canvasGroup, duration);
        }

        public void Dispose() =>
            _animationPlayingIsPossible = false;

        public Task PlayAnimationAsync(BaseAnimationData data) =>
            Task.WhenAll(
                _alphaAnimation.PlayAnimationAsync(new AlphaAnimationData { From = data.AlphaFrom, To = data.AlphaTo }), 
                PositionAnimation(data.PositionFrom, data.PositionTo));

        private async Task PositionAnimation(Vector2 from, Vector2 to)
        {
            float time = 0f;
            _windowRectTransform.anchoredPosition = from;
            while (_duration >= time && _animationPlayingIsPossible)
            {
                time += deltaTime;
                float t = Mathf.Clamp01(time / _duration);
                _windowRectTransform.anchoredPosition = Vector2.Lerp(from, to, t);
                await Task.Delay(Convert.ToInt32(deltaTime * 1000));
            }
            _windowRectTransform.anchoredPosition = to;
        }
    }

    public struct BaseAnimationData
    {
        public Vector2 PositionFrom; 
        public Vector2 PositionTo;
        public float AlphaFrom;
        public float AlphaTo;
    }
}

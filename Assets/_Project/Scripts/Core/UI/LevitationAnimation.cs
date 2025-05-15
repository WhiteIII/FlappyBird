using System;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Time;

namespace Project.Core.UI
{
    public class LevitationAnimation : IUIStartStopAnimation
    {
        private readonly Transform _targetTransform;
        private readonly float _frequency;
        private readonly float _amplitude;

        private bool _isPlaying = false;

        public LevitationAnimation(
            Transform targetTransform, 
            float frequency, 
            float amplitude)
        {
            _targetTransform = targetTransform;
            _frequency = frequency;
            _amplitude = amplitude;
        }

        public void StartAnimation()
        {
            _isPlaying = true;
            PlayAnimationAsync();
        }

        public void StopAnimation() =>
            _isPlaying = false;

        private async Task PlayAnimationAsync()
        {
            float time = 0f;
            
            while (_isPlaying)
            {
                _targetTransform.position = new Vector3(
                    _targetTransform.position.x,
                    _targetTransform.position.y + Mathf.Sin(time * _frequency) * _amplitude,
                    _targetTransform.position.z
                );

                time += deltaTime;
                await Task.Delay(Convert.ToInt32(deltaTime * 1000f));
            }
        }
    }
}

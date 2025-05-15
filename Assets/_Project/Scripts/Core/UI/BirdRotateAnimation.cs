using Project.Config;
using Project.Core.PlayerController;
using UnityEngine;
using static UnityEngine.Time;

namespace Project.Core.UI
{
    public class BirdRotateAnimation : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _fbxTransform;
        [SerializeField] private PlayerData _playerData;

        private Quaternion _maxRotation;
        private Quaternion _minRotation;
        private bool _animationIsPlaying = false;

        private void Awake()
        {
            _maxRotation = Quaternion.Euler(0, 0, _playerData.MaxRotationZ);
            _minRotation = Quaternion.Euler(0, 0, _playerData.MinRotationZ);

            _playerInput.OnClick += JumpOnClick;
        }

        private void OnDestroy() =>
            _playerInput.OnClick -= JumpOnClick;

        private void Update()
        {
            if (_animationIsPlaying)
            {
                _fbxTransform.rotation = Quaternion.Lerp(
                    _fbxTransform.rotation, 
                    _minRotation, 
                    _playerData.RotationSpeed * deltaTime);
            }
        }

        public void ResetRotation() =>
            _fbxTransform.rotation = Quaternion.identity;

        public void EnableAnimation() =>
            _animationIsPlaying = true;

        public void DisableAnimation() =>
            _animationIsPlaying = false;

        public void Jump() =>
            _fbxTransform.rotation = _maxRotation;
        
        private void JumpOnClick()
        {
            if (_animationIsPlaying)
                Jump();
        }
    }
}

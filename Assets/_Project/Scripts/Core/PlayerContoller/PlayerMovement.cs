using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Time;

namespace Project.Core.PlayerController
{
    public class PlayerMovement : IPlayerMovement, IInitializable, IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly Rigidbody2D _playerRigidBody;
        private readonly Transform _playerTransform;
        private readonly Vector2 _startPosition;
        private readonly float _jumpForce;

        private bool _isMoving = false;

        public PlayerMovement(
            IPlayerInput playerInput,
            Rigidbody2D playerRigidBody,
            float jumpForce,
            Transform playerTransform)
        {
            _playerInput = playerInput;
            _playerRigidBody = playerRigidBody;
            _jumpForce = jumpForce;
            _playerTransform = playerTransform;
            _startPosition = playerTransform.position;
        }

        public void Initialize() =>
            _playerInput.OnClick += Jump;

        public void Dispose() =>
            _playerInput.OnClick -= Jump;

        public void EnableMove() =>
            _isMoving = true;

        public void DisableMove() => 
            _isMoving = false;

        public void Jump()
        {
            if (_isMoving)
            {
                //_playerRigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _playerRigidBody.velocity = Vector2.up * _jumpForce;
            }
        }

        public void SetOnStartPosition()
        {
            _playerRigidBody.isKinematic = true;
            _playerTransform.position = _startPosition;
            _playerRigidBody.isKinematic = false;
        }
    }
}

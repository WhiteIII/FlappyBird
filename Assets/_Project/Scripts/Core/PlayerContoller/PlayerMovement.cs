using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Core.PlayerController
{
    public class PlayerMovement : IPlayerMovement, IInitializable, IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly Rigidbody2D _playerRigidBody;
        private readonly float _jumpForce;

        private bool _isMoving = false;

        public PlayerMovement(
            IPlayerInput playerInput,
            Rigidbody2D playerRigidBody,
            float jumpForce)
        {
            _playerInput = playerInput;
            _playerRigidBody = playerRigidBody;
            _jumpForce = jumpForce;
        }

        public void Initialize() =>
            _playerInput.OnClick += Jump;

        public void Dispose() =>
            _playerInput.OnClick -= Jump;

        public void EnableInput() =>
            _isMoving = true;

        public void DisableInput() => 
            _isMoving = false;

        public void EnableGravitation() =>
            _playerRigidBody.isKinematic = false;

        public void DisableGravitation() =>
            _playerRigidBody.isKinematic = true;

        public void Jump()
        {
            if (_isMoving)
                _playerRigidBody.velocity = Vector2.up * _jumpForce;
        }
    }
}

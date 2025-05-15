using Project.Core.UI;
using UnityEngine;

namespace Project.Core.PlayerController
{
    public class CalmState : IEnterState, IExitState
    {
        private readonly Rigidbody2D _playerRigidBody;
        private readonly BirdRotateAnimation _birdRotateAnimation;

        public CalmState(
            Rigidbody2D playerRigidBody, BirdRotateAnimation birdRotateAnimation)
        {
            _playerRigidBody = playerRigidBody;
            _birdRotateAnimation = birdRotateAnimation;
        }

        public void Enter()
        {
            _playerRigidBody.isKinematic = true;
            _birdRotateAnimation.DisableAnimation();
            _birdRotateAnimation.ResetRotation();
        }

        public void Exit()
        {
            _playerRigidBody.isKinematic = false;
            _birdRotateAnimation.EnableAnimation();
        }
    }
}

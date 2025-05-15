using Project.Core.UI;
using UnityEngine;

namespace Project.Core.PlayerController
{
    public class PlayerStateControllerFactory : IFactory<BaseStateController>
    {
        private BirdRotateAnimation _birdRotateAnimation;
        private readonly Rigidbody2D _playerRigidBody;

        public PlayerStateControllerFactory(
            BirdRotateAnimation birdRotateAnimation,
            Rigidbody2D playerRigidBody)
        {
            _birdRotateAnimation = birdRotateAnimation;
            _playerRigidBody = playerRigidBody;
        }

        public BaseStateController Create() =>
            new BaseStateController(
                new IState[]
                {
                    new CalmState(_playerRigidBody, _birdRotateAnimation),
                    new ActiveState()
                },
                new ITransition[]
                {
                    new Transition<CalmState, ActiveState>(),
                    new Transition<ActiveState, CalmState>()
                },
                typeof(CalmState));
    }
}

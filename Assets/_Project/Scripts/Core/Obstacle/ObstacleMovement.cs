using UnityEngine;
using static UnityEngine.Time;

namespace Project.Core.Obstacle
{
    public class ObstacleMovement : IUpdateable
    {
        private readonly Rigidbody2D _obstacleRigidBody;
        private readonly Transform _obstacleTransform;
        private readonly float _speed;

        public ObstacleMovement(
            Rigidbody2D obstacleRigidBody, 
            Transform obstacleTransform, 
            float speed)
        {
            _obstacleRigidBody = obstacleRigidBody;
            _obstacleTransform = obstacleTransform;
            _speed = speed;
        }

        public void Update() =>
            _obstacleRigidBody.position += Vector2.left * _speed * deltaTime;

        public void SetPosition(Vector3 position) => 
            _obstacleTransform.position = position;
    }
}
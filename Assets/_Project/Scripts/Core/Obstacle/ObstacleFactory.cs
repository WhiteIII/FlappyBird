using UnityEngine;

namespace Project.Core.Obstacle
{
    public class ObstacleFactory : IFactory<ObstacleObjectData> 
    {
        private readonly GameObject _obstaclePrefab;
        private readonly float _speed;

        public ObstacleFactory(
            GameObject obstaclePrefab,
            float speed)
        {
            _obstaclePrefab = obstaclePrefab;
            _speed = speed;
        }

        public ObstacleObjectData Create()
        {
            GameObject gameObject = GameObject.Instantiate(_obstaclePrefab);
            return new ObstacleObjectData
            {
                ObstacleGameObject = gameObject,
                ObstacleMovement = new ObstacleMovement(
                    gameObject.GetComponentInChildren<Rigidbody2D>(), 
                    gameObject.transform, 
                    _speed),
                ObstacleObjects = gameObject.GetComponentInChildren<ObstacleObjects>(),
                ObstacleReleaseController = gameObject.GetComponentInChildren<ObstacleReleaseController>(),
                ObstaclePointerController = gameObject.GetComponentInChildren<ObstaclePointerController>(),
            };
        }
    }
}

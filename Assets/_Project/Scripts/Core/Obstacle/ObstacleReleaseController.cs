using UnityEngine;

namespace Project.Core.Obstacle
{
    public class ObstacleReleaseController : MonoBehaviour
    {
        [SerializeField] private GameObject _releasedGameObject;
        
        private ObstacleSpawner _obstacleSpawner;

        public void Initialize(ObstacleSpawner obstacleSpawner) =>
            _obstacleSpawner = obstacleSpawner;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ObstacleDestroyer _))
                _obstacleSpawner.Release(_releasedGameObject);
        }
    }
}
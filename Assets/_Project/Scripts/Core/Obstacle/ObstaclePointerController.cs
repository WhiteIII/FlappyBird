using Project.Core.PlayerController;
using UnityEngine;

namespace Project.Core.Obstacle
{
    public class ObstaclePointerController : MonoBehaviour
    {
        private PlayerPointsCounter _playerPointsCounter;

        public void Initialize(PlayerPointsCounter playerPointsCounter) =>
            _playerPointsCounter = playerPointsCounter;

        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlayerHealth _))
                _playerPointsCounter.AddPoints();
        }
    }
}
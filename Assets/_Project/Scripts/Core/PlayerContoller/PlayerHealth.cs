using Project.Core.Obstacle;
using UnityEngine;

namespace Project.Core.PlayerController
{
    public class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        private BaseStateController _gameStateContoller;
        private bool _isKilling = false;

        public void Initialize(BaseStateController gameStateController) =>
            _gameStateContoller = gameStateController;

        public void Kill() =>
            _gameStateContoller.Translate(typeof(GameOverState));

        public void Revive() =>
            _isKilling = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ObstacleKillController _) && _isKilling == false)
            {
                _isKilling = true;
                Kill();
            }
        }
    }
}

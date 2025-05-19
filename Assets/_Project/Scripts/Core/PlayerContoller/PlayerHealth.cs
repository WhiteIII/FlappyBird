using Project.Core.Obstacle;
using UnityEngine;

namespace Project.Core.PlayerController
{
    public class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        private IGameCycle _gameCycle;
        private bool _isKilling = false;

        public void Initialize(IGameCycle gameCycle) =>
            _gameCycle = gameCycle;

        public void Kill() =>
            _gameCycle.StopGame();

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

using Project.Core.PlayerController;
using UnityEngine;

namespace Project.Core.Obstacle
{
    public class ObstacleKillController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TryGetComponent(out IPlayerHealth playerHealth))
                playerHealth.Kill();
        }
    }
}
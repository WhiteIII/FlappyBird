using UnityEngine;

namespace Project.Core.Obstacle
{
    public class ObstacleObjects : MonoBehaviour
    {
        [field: SerializeField] public GameObject TopObstacle { get; private set; }
        [field: SerializeField] public GameObject BottomObstacle { get; private set; }
    }
}
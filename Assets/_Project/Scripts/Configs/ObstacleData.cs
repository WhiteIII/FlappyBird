using UnityEngine;

namespace Project.Config
{
    [CreateAssetMenu(fileName = "ObstacleData", menuName = "Project/ObstacleData")]
    public class ObstacleData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 2f;
        [field: SerializeField] public float MinGapSize { get; private set; } = 3f;
        [field: SerializeField] public float MaxGapSize { get; private set; } = 5f;
        [field: SerializeField] public float MinVerticalPosition { get; private set; } = -1f;
        [field: SerializeField] public float MaxVerticalPosition { get; private set; } = 3f;

        [Tooltip("Spawn settings:")]
        [field: SerializeField] public float SpawnAmplitude { get; private set; } = 3f;
        [field: SerializeField] public int PoolMaxSize { get; private set; } = 30;
    }
}

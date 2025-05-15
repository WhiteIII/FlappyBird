using UnityEngine;

namespace Project.Config
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Project/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float JumpForce { get; private set; } = 1.5f;

        [Tooltip("Animation settings:")]
        [field: SerializeField] public float Amplitude { get; private set; } = 0.5f;
        [field: SerializeField] public float Frequency { get; private set; } = 0.3f;
        [field: SerializeField] public float MinRotationZ { get; private set; } = -60f;
        [field: SerializeField] public float MaxRotationZ { get; private set; } = 35f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 2.5f;
    }
}

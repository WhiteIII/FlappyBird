using UnityEngine;

namespace Project.Config
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "Project/WindowData")]
    public class WindowData : ScriptableObject
    {
        [Tooltip("Animation settings:")]
        [field: SerializeField] public float AnimationDuration { get; private set; } = 0.3f;
    }
}

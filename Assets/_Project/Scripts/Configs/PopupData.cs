using UnityEngine;

namespace Project.Config
{
    [CreateAssetMenu(fileName = "PopupData", menuName = "Project/PopupData")]
    public class PopupData : ScriptableObject
    {
        [Tooltip("Animation settings:")]
        [field: SerializeField] public float AnimationDuration { get; private set; } = 0.5f;
    }
}

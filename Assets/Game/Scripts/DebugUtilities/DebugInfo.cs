using UnityEngine;

namespace Game.DebugUtilities
{
    [CreateAssetMenu(fileName = "DebugInfo", menuName = "Utilities/Debug")]
    public class DebugInfo : ScriptableObject
    {
        [SerializeField] public bool enableLevelDebugging = true;
    }
}
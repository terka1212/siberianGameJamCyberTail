using Game.DebugUtilities;
using UnityEngine;
using VContainer;

namespace Game.DebugUtilities
{
    public class LevelsDebug : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiRectTransform;
        [SerializeField] private RectTransform _debugLevelsTransform;

        private DebugInfo _debugInfo;

        public void Start()
        {
            Instantiate(_debugLevelsTransform, _uiRectTransform);
        }
    }
}
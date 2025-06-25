using UnityEngine;
using VContainer;

namespace Game.UI
{
    [RequireComponent(typeof(Canvas))]
    public class GlobalUICanvas : MonoBehaviour
    {
        
        private Camera _mainCamera;

        [Inject]
        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }
        
        private void Start()
        {
            BindCanvas();
        }

        private void BindCanvas()
        {
            var uiCanvas = GetComponent<Canvas>();

            if (uiCanvas == null) return;
            uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            uiCanvas.worldCamera = _mainCamera;
            uiCanvas.planeDistance = 100;
        }
    }
}
using UnityEngine;

namespace Game.UI
{
    public class ToNextUIButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _fromUIRectTransform;
        [SerializeField] private RectTransform _toUIRectTransform;

        public void NextUI()
        {
            _fromUIRectTransform.gameObject.SetActive(false);
            _toUIRectTransform.gameObject.SetActive(true);
        }
    }
}
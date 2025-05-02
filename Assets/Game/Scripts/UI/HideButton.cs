using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class HideButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _duration;
        [SerializeField] private float _showXPos;
        [SerializeField] private float _hideXPos;
        
        private bool _isShow;

        private void OnHide()
        {
            _rectTransform.DOMoveX(_hideXPos, _duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        }

        private void OnShow()
        {
            _rectTransform.DOMoveX(_showXPos, _duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        }

        public void OnClick()
        {
            if (_isShow)
            {
                _isShow = false;
                OnHide();
            }
            else
            {
                _isShow = true;
                OnShow();
            }
            
        }
    }
}
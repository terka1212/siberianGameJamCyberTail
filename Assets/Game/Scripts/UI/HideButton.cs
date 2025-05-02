using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class HideButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _duration;
        [SerializeField] private float _showYPos;
        [SerializeField] private float _hideYPos;
        
        private bool _isShow;

        private void OnHide()
        {
            _rectTransform.DOMoveY(_hideYPos, _duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        }

        private void OnShow()
        {
            _rectTransform.DOMoveY(_showYPos, _duration).SetEase(Ease.InOutQuad).SetUpdate(true);
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
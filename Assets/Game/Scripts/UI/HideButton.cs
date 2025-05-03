using System;
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

        private static bool _isShow;

        private void Start()
        {
            if (_isShow)
            {
                _rectTransform.transform.position = new Vector3(_showXPos, _rectTransform.transform.position.y,
                    _rectTransform.transform.position.z);
            }
            else
            {
                _rectTransform.transform.position = new Vector3(_hideXPos, _rectTransform.transform.position.y,
                    _rectTransform.transform.position.z);
            }
        }

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
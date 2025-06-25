using System;
using System.Collections;
using DG.Tweening;
using Game.Data;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    public class Fade : MonoBehaviour
    {
        private Image image;

        private FadeSettings _fadeSettings;
        
        public void Init(FadeSettings fadeSettings)
        {
            _fadeSettings = fadeSettings;
        }

        public void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Start()
        {
            image.raycastTarget = false;
        }

        public IEnumerator FadeIn()
        {
            image.raycastTarget = true;
            yield return image.DOFade(1f, _fadeSettings.durationIn).SetEase(_fadeSettings.easeIn)
                .WaitForCompletion();
        }

        public IEnumerator FadeOut()
        {
            yield return image.DOFade(0f, _fadeSettings.durationOut).SetEase(_fadeSettings.easeOut)
                .WaitForCompletion();
            image.raycastTarget = false;
        }
    }
}
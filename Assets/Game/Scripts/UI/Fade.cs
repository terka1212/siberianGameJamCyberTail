using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    public class Fade : MonoBehaviour
    {
        private Image image;

        private FadeSettings _fadeSettings;

        [Inject]
        public void Construct(FadeSettings fadeSettings)
        {
            _fadeSettings = fadeSettings;
        }

        public void Awake()
        {
            image = GetComponent<Image>();
        }

        public IEnumerator FadeIn()
        {
            yield return image.DOFade(1f, _fadeSettings.inAndOutDuration / 2).SetEase(_fadeSettings.easeIn)
                .WaitForCompletion();
        }

        public IEnumerator FadeOut()
        {
            yield return image.DOFade(0f, _fadeSettings.inAndOutDuration / 2).SetEase(_fadeSettings.easeOut)
                .WaitForCompletion();
        }
    }
}
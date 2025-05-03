using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Image))]
    public class Fade : MonoBehaviour
    {
        private Image image;

        private const float duration = 0.5f;
        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public IEnumerator FadeIn()
        {
            yield return image.DOFade(1f, duration).WaitForCompletion();
        }
        
        public IEnumerator FadeOut()
        {
            yield return image.DOFade(0f, duration).WaitForCompletion();
        }
    }
}
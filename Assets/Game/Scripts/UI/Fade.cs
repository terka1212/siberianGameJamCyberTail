using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public static Fade instance;

        private const float duration = 0.5f;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public static IEnumerator FadeIn()
        {
            yield return instance.image.DOFade(1f, duration).WaitForCompletion();
        }
        
        public static IEnumerator FadeOut()
        {
            yield return instance.image.DOFade(0f, duration).WaitForCompletion();
        }

    }
}
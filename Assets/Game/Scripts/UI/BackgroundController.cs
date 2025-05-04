using System.Collections;
using Game.Events;
using Game.SceneManagement;
using Game.Utils;
using UnityEngine;

namespace Game.UI
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;

        public void ChangeSpriteWithFade(Sprite sprite)
        {
            StartCoroutine(ChangeSpriteWithFadeCoroutine(sprite));
        }
        
        public void ChangeSpriteWithoutFade(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        private IEnumerator ChangeSpriteWithFadeCoroutine(Sprite sprite)
        {
            SceneLoader.isSceneLoading = true;
            EventManager.InvokeStartSceneLoading();
            yield return CoroutineHandler.instance.StartCoroutine(Fade.FadeIn());

            spriteRenderer.sprite = sprite;

            SceneLoader.isSceneLoading = false;
            yield return CoroutineHandler.instance.StartCoroutine(Fade.FadeOut());
        }
    }
}
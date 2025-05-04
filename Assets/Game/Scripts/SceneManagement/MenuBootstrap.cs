using DG.Tweening;
using Game.Audio;
using UnityEngine;

namespace Game.SceneManagement
{
    public class MenuBootstrap : MonoBehaviour
    {
        private void Start()
        {
            AudioStorage.PlayGlobalMusic("bomba", new FadeSettings()
            {
                durationIn = 0.5f,
                durationOut = 0.5f,
                easeIn = Ease.InQuad,
                easeOut = Ease.OutQuad,
            });
        }
    }
}
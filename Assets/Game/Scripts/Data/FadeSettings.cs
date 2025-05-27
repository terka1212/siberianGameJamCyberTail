using DG.Tweening;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "NewFadeSettings", menuName = "Utilities/Fade")]
    public class FadeSettings : ScriptableObject
    {
        public float durationIn = 0.5f;
        public float durationOut = 0.5f;

        public Ease easeIn = Ease.Linear;
        public Ease easeOut = Ease.Linear;
    }
}
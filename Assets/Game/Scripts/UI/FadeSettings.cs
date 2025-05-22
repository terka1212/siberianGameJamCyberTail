using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(fileName = "DefaultFadeSettings", menuName = "Utilities/Fade", order = 0)]
    public class FadeSettings : ScriptableObject
    {
        [Range(0.1f, 10f)]
        [SerializeField] public float inAndOutDuration = 0.5f;
        [SerializeField] public Ease easeIn = Ease.InOutQuad;
        [SerializeField] public Ease easeOut = Ease.InOutQuad;
    }
}
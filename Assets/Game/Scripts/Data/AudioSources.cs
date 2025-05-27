using System;
using UnityEngine;

namespace Game.Data
{
    public class AudioSources : MonoBehaviour
    {
        [SerializeField] public AudioSource MusicSource;
        [SerializeField] public AudioSource EffectSource;
        [SerializeField] public AudioSource DialogueSource;
        [SerializeField] public AudioSource UISource;
        [SerializeField] public AudioSource RandomnessEffectSource;
        [SerializeField] public AudioSource RandomnessUISource;

        public FadeSettings FadeSettings { get; private set; }

        public void Init(FadeSettings fadeSettings)
        {
            FadeSettings = fadeSettings;
        }
    }
}
using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class Sound
    {
        public string name;
        
        public float volume;

        public AudioClip clip;

        public float minPitch;
        
        public float maxPitch;
    }

    public enum EffectSoundType
    {
        Effect,
        Dialogue,
        UI
    }
    
}

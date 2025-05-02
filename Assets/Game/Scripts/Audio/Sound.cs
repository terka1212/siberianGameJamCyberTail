using System;
using UnityEngine;

namespace Game.Audio
{
    [Serializable]
    public class Sound
    {
        public string name;
        
        public float volume;

        public AudioClip clip;
    }
}

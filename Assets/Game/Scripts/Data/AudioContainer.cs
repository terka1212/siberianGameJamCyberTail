using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "NewAudioContainer", menuName = "Utilities/Audio/AudioContainer")]
    public class AudioContainer : ScriptableObject
    {
        [SerializeField] private List<Sound> musicSounds;
        [SerializeField] private List<Sound> sfxSounds;
        [NonSerialized] private List<Sound> _sounds;

        public void Init()
        {
            if(_sounds == null || _sounds.Count > 0) 
                _sounds = new List<Sound>();
            _sounds.AddRange(musicSounds);
            _sounds.AddRange(sfxSounds);
        }

        public bool TryFindSound(out Sound sound, string soundName)
        {
            sound = null;
            if (soundName == null) return false;
            if (_sounds.Count == 0) return false;

            sound = FindSound(_sounds, soundName);

            return sound != null;
        }

        private static Sound FindSound(List<Sound> sounds, string name)
        {
            return sounds.First(sound => sound.name == name);
        }

        private void OnDestroy()
        {
            _sounds = null;
        }
    }
}
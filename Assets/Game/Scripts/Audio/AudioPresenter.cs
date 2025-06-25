using Game.Data;
using UnityEngine;
using VContainer;

namespace Game.Audio
{
    public class AudioPresenter
    {
        private AudioService _audioService;

        [Inject]
        public AudioPresenter(AudioService audioService)
        {
            _audioService = audioService;
        }

        public void PlayMusic(string musicName, FadeSettings fadeSettings = null)
        {
            if (fadeSettings == null)
                _audioService.PlayMusic(musicName);
            else
                _audioService.PlayMusicWithFade(musicName, fadeSettings);
        }

        public void PlaySound(string soundName, EffectSoundType soundType = EffectSoundType.Effect,
            bool isRandomness = false)
        {
            if (isRandomness) 
                _audioService.PlayRandomnessEffectSound(soundName, soundType);
            else 
                _audioService.PlaySound(soundName, soundType);
        }
    }
}
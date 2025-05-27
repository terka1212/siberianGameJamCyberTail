using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace Game.Audio
{
    public class AudioSettingsService
    {
        private AudioMixerGroup _audioMixerGroup;

        private const float MAX_MIXER_VOLUME = 0;
        private const float MIN_MIXER_VOLUME = -80;

        [Inject]
        public AudioSettingsService(AudioMixerGroup audioMixerGroup)
        {
            _audioMixerGroup = audioMixerGroup;
        }

        public void SetMasterVolume(float volume)
        {
            _audioMixerGroup.audioMixer.SetFloat("MasterVolume",
                Mathf.Lerp(MIN_MIXER_VOLUME, MAX_MIXER_VOLUME, volume));
        }

        public void SetSFXVolume(float volume)
        {
            _audioMixerGroup.audioMixer.SetFloat("EffectsVolume",
                Mathf.Lerp(MIN_MIXER_VOLUME, MAX_MIXER_VOLUME, volume));
        }

        public void SetDialogueVolume(float volume)
        {
            _audioMixerGroup.audioMixer.SetFloat("DialogueVolume",
                Mathf.Lerp(MIN_MIXER_VOLUME, MAX_MIXER_VOLUME, volume));
        }

        public void SetMusicVolume(float volume)
        {
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume",
                Mathf.Lerp(MIN_MIXER_VOLUME, MAX_MIXER_VOLUME, volume));
        }
    }
}
using System.Collections;
using DG.Tweening;
using Game.Data;
using Game.Events;
using Game.Utils;
using Game.Validation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Audio
{
    public class AudioService
    {
        private AudioContainer _audioContainer;
        private AudioSources _audioSources;
        private CoroutineHandler _coroutineHandler;
        private EventManager _eventManager;
        private Coroutine _cachedCoroutine;

        [Inject]
        public AudioService(AudioContainer audioContainer, AudioSources audioSources, CoroutineHandler coroutineHandler,
            EventManager eventManager)
        {
            _audioContainer = audioContainer;
            _audioSources = audioSources;
            _coroutineHandler = coroutineHandler;
            _eventManager = eventManager;
        }

        public void PlayMusic(string musicName)
        {
            if (!_audioContainer.TryFindSound(out var sound, musicName))
            {
                _eventManager.InvokeOnEffectDontFound(ValidationMessages.AUDIO_MUSIC_DONT_FOUND);
                return;
            }

            SetAndPlayMusic(sound);
        }

        public void PlayMusicWithFade(string musicName, FadeSettings fadeSettings)
        {
            if (!_audioContainer.TryFindSound(out var sound, musicName))
            {
                _eventManager.InvokeOnEffectDontFound(ValidationMessages.AUDIO_MUSIC_DONT_FOUND);
                return;
            }

            if (_cachedCoroutine != null) _coroutineHandler.StopCoroutine(_cachedCoroutine);
            _cachedCoroutine = _coroutineHandler.StartCoroutine(StartMusicWithFade(sound, fadeSettings));
        }

        public void PlaySound(string soundName, EffectSoundType soundType = EffectSoundType.Effect)
        {
            if (!_audioContainer.TryFindSound(out var sound, soundName))
            {
                _eventManager.InvokeOnEffectDontFound(ValidationMessages.AUDIO_EFFECT_DONT_FOUND);
                return;
            }

            switch (soundType)
            {
                case EffectSoundType.Effect:
                    _audioSources.EffectSource.PlayOneShot(sound.clip, sound.volume); break;
                case EffectSoundType.Dialogue:
                    _audioSources.DialogueSource.PlayOneShot(sound.clip, sound.volume); break;
                case EffectSoundType.UI:
                    _audioSources.UISource.PlayOneShot(sound.clip, sound.volume); break;
                default:
                    _eventManager.InvokeOnSoundTypeIsntHandle(); break;
            }
        }

        public void PlayRandomnessEffectSound(string soundName, EffectSoundType soundType = EffectSoundType.Effect)
        {
            if (!_audioContainer.TryFindSound(out var sound, soundName))
            {
                _eventManager.InvokeOnEffectDontFound(ValidationMessages.AUDIO_EFFECT_DONT_FOUND);
                return;
            }

            switch (soundType)
            {
                case EffectSoundType.Effect:
                    _audioSources.RandomnessEffectSource.pitch = Random.Range(sound.minPitch, sound.maxPitch);
                    _audioSources.RandomnessEffectSource.PlayOneShot(sound.clip, sound.volume);
                    break;
                case EffectSoundType.UI:
                    _audioSources.RandomnessUISource.pitch = Random.Range(sound.minPitch, sound.maxPitch);
                    _audioSources.RandomnessUISource.PlayOneShot(sound.clip, sound.volume);
                    break;
                default:
                    _eventManager.InvokeOnSoundTypeIsntHandle(); break;
            }
        }

        private IEnumerator StartMusicWithFade(Sound sound, FadeSettings fadeSettings)
        {
            yield return _audioSources.MusicSource.DOFade(0, fadeSettings.durationIn)
                .SetEase(fadeSettings.easeIn)
                .SetUpdate(true)
                .WaitForCompletion();

            SetAndPlayMusic(sound, false);
            yield return _audioSources.MusicSource.DOFade(sound.volume, fadeSettings.durationOut)
                .SetEase(fadeSettings.easeOut)
                .SetUpdate(true)
                .WaitForCompletion();

            _cachedCoroutine = null;
        }

        private void SetAndPlayMusic(Sound sound, bool withVolume = true)
        {
            if (withVolume)
                _audioSources.MusicSource.volume = sound.volume;
            else 
                _audioSources.MusicSource.volume = 0;
            _audioSources.MusicSource.clip = sound.clip;
            _audioSources.MusicSource.Play();
        }
    }
}
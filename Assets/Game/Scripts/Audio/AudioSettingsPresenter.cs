using Game.Events;

namespace Game.Audio
{
    public class AudioSettingsPresenter
    {
        private AudioSettingsService _audioService;

        private EventManager _eventManager;

        public AudioSettingsPresenter(AudioSettingsService audioService, EventManager eventManager)
        {
            _audioService = audioService;
            _eventManager = eventManager;
        }

        public void SetVolume(float volume, VolumeSettingType volumeType)
        {
            switch (volumeType)
            {
                case VolumeSettingType.Master: _audioService.SetMasterVolume(volume); break;
                case VolumeSettingType.SFX: _audioService.SetSFXVolume(volume); break;
                case VolumeSettingType.Dialogue: _audioService.SetDialogueVolume(volume); break;
                case VolumeSettingType.Music: _audioService.SetMusicVolume(volume); break;
                default: _eventManager.InvokeOnSoundSettingTypeIsntHandle(); break;
            }
        }
    }

    public enum VolumeSettingType
    {
        Master,
        SFX,
        Dialogue,
        Music
    }
}
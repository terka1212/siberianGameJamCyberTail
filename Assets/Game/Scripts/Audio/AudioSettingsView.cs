using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Audio
{
    public class AudioSettingsView : MonoBehaviour
    {
        [SerializeField] public Slider masterVolumeSlider;
        [SerializeField] public Slider musicVolumeSlider;
        [SerializeField] public Slider sfxVolumeSlider;
        [SerializeField] public Slider dialogueVolumeSlider;

        private AudioSettingsPresenter _audioSettingsPresenter;

        [Inject]
        public void Construct(AudioSettingsPresenter audioSettingsPresenter)
        {
            _audioSettingsPresenter = audioSettingsPresenter;
        }

        private void Awake()
        {
            masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChange);
            musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChange);
            sfxVolumeSlider.onValueChanged.AddListener(HandleSfxVolumeChange);
            dialogueVolumeSlider.onValueChanged.AddListener(HandleDialogueVolumeChange);
        }

        private void HandleMasterVolumeChange(float value)
        {
            _audioSettingsPresenter.SetVolume(value, VolumeSettingType.Master);
        }

        private void HandleMusicVolumeChange(float value)
        {
            _audioSettingsPresenter.SetVolume(value, VolumeSettingType.Music);
        }

        private void HandleSfxVolumeChange(float value)
        {
            _audioSettingsPresenter.SetVolume(value, VolumeSettingType.SFX);
        }

        private void HandleDialogueVolumeChange(float value)
        {
            _audioSettingsPresenter.SetVolume(value, VolumeSettingType.Dialogue);
        }

        private void OnDestroy()
        {
            masterVolumeSlider.onValueChanged.RemoveListener(HandleMasterVolumeChange);
            musicVolumeSlider.onValueChanged.RemoveListener(HandleMusicVolumeChange);
            sfxVolumeSlider.onValueChanged.RemoveListener(HandleSfxVolumeChange);
            dialogueVolumeSlider.onValueChanged.RemoveListener(HandleDialogueVolumeChange);
        }
    }
}
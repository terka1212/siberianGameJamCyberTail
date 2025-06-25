using Game.Audio;
using Game.Data;
using UnityEngine;
using VContainer;

namespace Game.SceneManagement
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private FadeSettings mainThemeFadeSettings;
    
        private AudioPresenter _audioPresenter;
        private ScenePresenter _scenePresenter;

        [Inject]
        public void Construct(AudioPresenter audioPresenter, ScenePresenter scenePresenter)
        {
            _audioPresenter = audioPresenter;
            _scenePresenter = scenePresenter;
        }
    
        void Start()
        {
            PlayMainThemeMusic();
            _scenePresenter.InvokeTransition(SceneName.Bootstrap, SceneName.Menu);
        }

        private void PlayMainThemeMusic()
        {
            _audioPresenter.PlayMusic("MainTheme", mainThemeFadeSettings);
        }
    }
}

using Game.Audio;
using Game.Data;
using Game.DebugUtilities;
using Game.Events;
using Game.SceneManagement;
using Game.UI;
using Game.Utils;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        [Header("Camera Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;
        
        [Header("UI Settings")]
        [SerializeField] private GlobalUICanvas _globalUICanvas;
        [SerializeField] private Fade _fade;
        [SerializeField] private FadeSettings _fadeSettings;
        
        [Header("Point and Click Settings")] 
        [SerializeField] private bool _isPointAndClickBlockedOnStartScene;
        [SerializeField] private float _maxRaycastDistance;
        
        [Header("Dialogue Settings")]
        [SerializeField] private float _typeSpeed;
        [SerializeField] private float _maxTypeTime;
        
        [Header("Sound Settings")]
        [SerializeField] private AudioContainer _audioContainer;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioSources _audioSources;
        [SerializeField] private FadeSettings _audioFadeSettings;
        
        [Header("Debug Settings")]
        [SerializeField] private GameObject _levelsDebug;
        
        private GlobalUICanvas _globalUICanvasInstance;
        
        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            BindCamera(builder);
            BindEventSystem(builder);
            _globalUICanvasInstance = BindGlobalUICanvas(builder);
            BindCoroutineHandler(builder);
            BindEventManager(builder);
            BindFadeSystem(builder);
            BindSceneSystem(builder);
            BindPointAndClickData(builder);
            BindDialogueSystem(builder);
            BindAudioSystem(builder);
            BindScopedLifecycleManager(builder);
            BindDebugInfo(builder);
        }

        private void BindCamera(IContainerBuilder builder)
        {
            var camera = Instantiate(_camera);
            camera.name = "MainCamera";
            DontDestroyOnLoad(camera.gameObject);
            builder.RegisterComponent(camera);
        }

        private void BindEventSystem(IContainerBuilder builder)
        {
            var eventSystem = Instantiate(_eventSystem);
            DontDestroyOnLoad(eventSystem);
            
            builder.RegisterComponent(_eventSystem);
        }

        private GlobalUICanvas BindGlobalUICanvas(IContainerBuilder builder)
        {
            var globalUI = Instantiate(_globalUICanvas);
            DontDestroyOnLoad(globalUI);
            builder.RegisterComponent(globalUI);
            return globalUI;
        }

        private void BindCoroutineHandler(IContainerBuilder builder)
        {
            var coroutineHandler = new GameObject("CoroutineHandler", typeof(CoroutineHandler)).GetComponent<CoroutineHandler>();
            DontDestroyOnLoad(coroutineHandler);
            builder.RegisterComponent(coroutineHandler);
        }

        private void BindEventManager(IContainerBuilder builder)
        {
            builder.Register<EventManager>(Lifetime.Singleton);
        }

        private void BindFadeSystem(IContainerBuilder builder)
        {
            var fade = Instantiate(_fade, _globalUICanvasInstance.transform);
            fade.Init(_fadeSettings);
            builder.RegisterComponent(fade);

            builder.Register<FadeService>(Lifetime.Singleton);
        }

        private void BindSceneSystem(IContainerBuilder builder)
        {
            builder.Register<SceneData>(Lifetime.Singleton);
            builder.Register<SceneService>(Lifetime.Singleton);
            builder.Register<ScenePresenter>(Lifetime.Singleton);
        }

        private void BindPointAndClickData(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PointAndClickData>()
                .WithParameter(_maxRaycastDistance)
                .WithParameter(_isPointAndClickBlockedOnStartScene)
                .AsSelf();
        }

        private void BindDialogueSystem(IContainerBuilder builder)
        {
            builder.Register<DialogueData>(Lifetime.Singleton)
                .WithParameter(_typeSpeed)
                .WithParameter(_maxTypeTime);
        }

        private void BindAudioSystem(IContainerBuilder builder)
        {
            //Init and register audioContainer
            _audioContainer.Init();
            builder.RegisterInstance(_audioContainer);
            
            builder.RegisterInstance(_audioMixerGroup);
            
            //instantiate and register audioSources manually
            var audioSources = Instantiate(_audioSources);
            _audioSources.Init(_audioFadeSettings);
            DontDestroyOnLoad(audioSources);
            builder.RegisterComponent(audioSources);
            
            builder.Register<AudioService>(Lifetime.Singleton);
            builder.Register<AudioPresenter>(Lifetime.Singleton);
            builder.Register<AudioSettingsService>(Lifetime.Singleton);
            builder.Register<AudioSettingsPresenter>(Lifetime.Singleton);
        }

        private void BindScopedLifecycleManager(IContainerBuilder builder)
        {
            builder.Register<ScopedLifecycleManager>(Lifetime.Singleton);
        }

        private void BindDebugInfo(IContainerBuilder builder)
        {
            var config = Resources.Load<DebugInfo>("DebugInfo");
            if (config.enableLevelDebugging)
            {
                builder.RegisterBuildCallback(resolver =>
                {
                    var levelsDebug = resolver.Instantiate(_levelsDebug, _globalUICanvasInstance.transform);
                    DontDestroyOnLoad(levelsDebug);
                });
            }
        }
    }
}
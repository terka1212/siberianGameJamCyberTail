using Game.Audio;
using Game.Data;
using Game.Dialogues;
using Game.Dialogues.NPC;
using Game.Events;
using Game.Navigation;
using Game.SceneManagement;
using Game.UI;
using Game.Utils;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        [Header("UI Settings")]
        [SerializeField] private GlobalUICanvas _globalUICanvas;
        [SerializeField] private Fade _fade;
        [SerializeField] private FadeSettings _fadeSettings;

        [Header("Point and Click Settings")] 
        [SerializeField] private bool _isPointAndClickBlockedOnStart = true;
        [SerializeField] private float _maxRaycastDistance;
        
        [Header("Dialogue Settings")]
        [SerializeField] private float _typeSpeed;
        [SerializeField] private float _maxTypeTime;
        
        [Header("Sound Settings")]
        [SerializeField] private AudioContainer _audioContainer;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioSources _audioSources;
        [SerializeField] private FadeSettings _audioFadeSettings;
        

        protected override void Configure(IContainerBuilder builder)
        {
            var globalUI = BindGlobalUICanvas(builder);
            BindCoroutineHandler(builder);
            BindEventManager(builder);
            BindFadeSystem(builder, globalUI);
            BindSceneSystem(builder);
            BindPointAndClickSystem(builder);
            BindInteractableHandlerSystem(builder);
            BindDialogueSystem(builder);
            BindAudioSystem(builder);
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
            builder.RegisterComponentOnNewGameObject<CoroutineHandler>(Lifetime.Singleton).DontDestroyOnLoad();
        }

        private void BindEventManager(IContainerBuilder builder)
        {
            builder.Register<EventManager>(Lifetime.Singleton);
        }

        private void BindFadeSystem(IContainerBuilder builder, GlobalUICanvas globalUICanvas)
        {
            var fade = Instantiate(_fade, globalUICanvas.transform);
            fade.Init(_fadeSettings);
            DontDestroyOnLoad(fade);
            builder.RegisterComponent(fade);

            builder.Register<FadeService>(Lifetime.Singleton);
        }

        private void BindSceneSystem(IContainerBuilder builder)
        {
            builder.Register<SceneData>(Lifetime.Singleton);
            builder.Register<SceneService>(Lifetime.Singleton);
            builder.Register<ScenePresenter>(Lifetime.Singleton);
        }

        private void BindPointAndClickSystem(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlayerNavMeshAgentService>();
            builder.Register<PointAndClickData>(Lifetime.Singleton)
                .WithParameter(_maxRaycastDistance)
                .WithParameter(_isPointAndClickBlockedOnStart);
            builder.Register<PointAndClickService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PointAndClickPresenter>();
        }

        private void BindInteractableHandlerSystem(IContainerBuilder builder)
        {
            builder.Register<InteractableHandlingService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InteractableHandlingPresenter>();
        }

        private void BindDialogueSystem(IContainerBuilder builder)
        {
            builder.Register<DialogueData>(Lifetime.Singleton)
                .WithParameter(_typeSpeed)
                .WithParameter(_maxTypeTime);
            builder.Register<DialogueService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<DialoguePresenter>();
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
    }
}
using Game.Data;
using Game.Events;
using Game.Navigation;
using Game.SceneManagement;
using Game.UI;
using Game.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class RootLifeTimeScope : LifetimeScope
    {
        [SerializeField] private GlobalUICanvas _globalUICanvas;
        [SerializeField] private Fade _fade;
        [SerializeField] private FadeSettings _fadeSettings;


        protected override void Configure(IContainerBuilder builder)
        {
            var globalUI = BindGlobalUICanvas(builder);
            BindCoroutineHandler(builder);
            BindEventManager(builder);
            BindFadeSystem(builder, globalUI);
            BindSceneSystem(builder);
            BindPointAndClickSystem(builder);
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
            builder.RegisterInstance(_fadeSettings);
            
            var fade = Instantiate(_fade, globalUICanvas.transform);
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
            builder.Register<PointAndClickService>(Lifetime.Singleton);
        }
    }
}
using Game.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.ScopedLifecycle.Scopes
{
    public class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private RectTransform uiRectTransform;

        protected override void Configure(IContainerBuilder builder)
        {
            BindAudioSystem(builder);

            Debug.Log("SceneLifetimeScope: Configuration - Completed!");
        }

        private void BindAudioSystem(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<AudioSettingsView>();
        }

    }
}
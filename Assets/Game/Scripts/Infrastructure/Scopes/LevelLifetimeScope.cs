using Game.Dialogues;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class LevelLifetimeScope : LifetimeScope
    {
        [SerializeField] private RectTransform uiRectTransform;
        [SerializeField] private Canvas dialogueCanvas;
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindNavMeshSystem(builder);
            BindDialogueSystem(builder);
        }

        private void BindNavMeshSystem(IContainerBuilder builder)
        {
            builder.Register<NavMeshAgent>(Lifetime.Scoped);
        }

        private void BindDialogueSystem(IContainerBuilder builder)
        {
            builder.Register<DialogueDistributor>(Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(dialogueCanvas, Lifetime.Scoped)
                .UnderTransform(uiRectTransform);
            builder.Register<DialogueView>(Lifetime.Scoped);
        }
    }
}
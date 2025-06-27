using Game.Dialogues;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class DialogueSystemLifetimeScope : LifetimeScope
    {
        [SerializeField] private RectTransform uiRectTransform;
        [SerializeField] private Canvas dialogueCanvas;
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindDialogues(builder);
        }

        private void BindDialogues(IContainerBuilder builder)
        {
            builder.Register<DialogueDistributor>(Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(dialogueCanvas, Lifetime.Scoped)
                .UnderTransform(uiRectTransform);
            builder.Register<DialogueView>(Lifetime.Scoped);
            builder.Register<DialogueService>(Lifetime.Scoped);
            builder.Register<DialoguePresenter>(Lifetime.Scoped);
        }
    }
}
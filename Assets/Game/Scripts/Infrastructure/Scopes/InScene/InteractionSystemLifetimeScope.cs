using Game.Dialogues.NPC;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class InteractionSystemLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            BindInteractionSystem(builder);
        }
        
        private void BindInteractionSystem(IContainerBuilder builder)
        {
            builder.Register<InteractableHandlingService>(Lifetime.Scoped);
            builder.Register<InteractableHandlingPresenter>(Lifetime.Scoped);
        }
    }
}
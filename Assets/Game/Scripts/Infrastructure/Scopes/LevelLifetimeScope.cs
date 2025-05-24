using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class LevelLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            BindNavMeshSystem(builder);
        }

        private void BindNavMeshSystem(IContainerBuilder builder)
        {
            builder.Register<NavMeshAgent>(Lifetime.Scoped);
        }
    }
}
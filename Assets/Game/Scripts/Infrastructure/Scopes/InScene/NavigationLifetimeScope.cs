using Game.GameObjects;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Scopes
{
    public class NavigationLifetimeScope : LifetimeScope
    {
        [SerializeField] private NavMeshAgent agent;

        protected override void Configure(IContainerBuilder builder)
        {
            BindNavigationSystem(builder);
        }

        private void BindNavigationSystem(IContainerBuilder builder)
        {
            if (agent != null)
            {
                builder.RegisterComponent(agent);
                builder.RegisterEntryPoint<NavMeshAgentService>().As<INavMeshAgentService>();
            }
            else
            {
                builder.Register<INavMeshAgentService, NullNavMeshAgentService>(Lifetime.Scoped);
                Debug.LogWarning("Null navmesh agent, register NullNavMeshAgentService");
            }
            builder.Register<PointAndClickService>(Lifetime.Scoped);
            builder.RegisterEntryPoint<PointAndClickPresenter>();
        }
        
    }
}
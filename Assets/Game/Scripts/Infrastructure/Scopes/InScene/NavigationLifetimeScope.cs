using Game.GameObjects;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.ScopedLifecycle.Scopes
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
                builder.Register<INavMeshAgentService, NavMeshAgentService>(Lifetime.Scoped);
            }
            else
            {
                builder.Register<INavMeshAgentService, NullNavMeshAgentService>(Lifetime.Scoped);
                Debug.LogWarning("Null navmesh agent, register NullNavMeshAgentService");
            }
            builder.Register<PointAndClickService>(Lifetime.Scoped);
            builder.Register<PointAndClickPresenter>(Lifetime.Scoped);
            
            builder.RegisterBuildCallback(container =>
            {
                var pacPresenter = container.Resolve<PointAndClickPresenter>();
            });
        }
        
    }
}
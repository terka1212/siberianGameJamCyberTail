using System.Collections.Generic;
using Game.Data;
using Game.SceneManagement;
using UnityEngine;
using VContainer;

namespace Game.GameObjects
{
    public class LevelJumper : MonoBehaviour, IInteractable, IApproachable
    {
        [Header("Settings")]
        [SerializeField] private SceneName jumpToScene;
        [SerializeField] private List<Transform> possibleDestinations;

        private ScenePresenter _scenePresenter;

        [Inject]
        public void Construct(ScenePresenter scenePresenter)
        {
            _scenePresenter = scenePresenter;
        }

        public void Interact()
        {
            _scenePresenter.InvokeTransition(_scenePresenter.GetCurrentScene(), jumpToScene);
        }

        public List<Vector3> GetPossibleDestinationPoints()
        {
            return possibleDestinations.ConvertAll(x => x.position);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            foreach (var destination in possibleDestinations)
            {
                Gizmos.DrawSphere(destination.position, 0.2f);
            }
        }
    }
}
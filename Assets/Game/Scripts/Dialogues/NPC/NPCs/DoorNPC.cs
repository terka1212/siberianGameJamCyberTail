using Game.Data;
using Game.SceneManagement;
using Game.Utils;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public class DoorNPC : NPC
    {
        [SerializeField] private SceneName moveToScene;
        
        //direction where player coming from
        [SerializeField] Vector3 comingPlayerDirection;
        
        public override void Interact()
        {
            StartCoroutine(SceneLoader.LoadScene(moveToScene));
        }

        public override Vector3 GetFrontPoint(LayerMask mask)
        {
            //11 - navigation layer
            if (Physics.Raycast(transform.position + comingPlayerDirection, Vector3.down, out RaycastHit hit, 30f, mask))
            {
                return hit.point;
            }
            return transform.position + comingPlayerDirection;
        }
    }
}
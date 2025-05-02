using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField] public int id;
        [SerializeField] private List<DialogueText> dialogues;

        public abstract void Interact();

        public void Initialize()
        {
            NPCProgressTracker.InitDialogue(this, dialogues);
            ProgressStorage.SetProgress(id, 0);
        }
        
        public Vector2 GetFrontPoint()
        {
            //11 - navigation layer
            if(Physics.Raycast(transform.position + Vector3.back, Vector3.down, out RaycastHit hit, 10f, 11))
                return hit.point;
            return transform.position;
        }
    }
}
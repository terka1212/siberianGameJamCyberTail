﻿using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField] public int id;
        [SerializeField] private int initProgress;
        [SerializeField] private List<DialogueText> dialogues;

        public abstract void Interact();

        public void Initialize()
        {
            NPCProgressTracker.InitDialogue(id, dialogues);
            ProgressStorage.SetProgress(id, initProgress);
        }
        
        public virtual Vector3 GetFrontPoint(LayerMask mask)
        {
            //11 - navigation layer
            if (Physics.Raycast(transform.position + Vector3.back, Vector3.down, out RaycastHit hit, 30f, mask))
            {
                return hit.point;
            }
            return transform.position + Vector3.back;
        }
    }
}
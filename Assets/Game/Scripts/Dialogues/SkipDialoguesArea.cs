using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Dialogues
{
    public class SkipDialoguesArea : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private DialogueController dialogueController;
        [NonSerialized] public DialogueText dialogueText;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            dialogueController.DisplayNextParagraph(dialogueText);
        }
    }
}
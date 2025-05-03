using Game.Scripts;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public class TestNPC : NPC, ITalkable
    {
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private SkipDialoguesArea skipDialoguesArea;
        
        private void Awake()
        {
            Initialize();
        }

        public override void Interact()
        {
            var dialogueText = NPCProgressTracker.GetDialogue(id);
            Talk(dialogueText);
        }

        public void InitDialogueArea(DialogueText dialogueText)
        {
            skipDialoguesArea.dialogueText = dialogueText;
        }

        public void Talk(DialogueText dialogueText)
        {
            InitDialogueArea(dialogueText);
            dialogueController.DisplayNextParagraph(dialogueText);
            Debug.Log("Talking");
        }
    }
}
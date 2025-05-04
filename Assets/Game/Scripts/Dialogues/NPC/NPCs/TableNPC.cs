using Game.UI;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public class TableNPC : NPC, ITalkable
    {
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private SkipDialoguesArea skipDialoguesArea;
        [SerializeField] private BackgroundController backgroundController;
        [SerializeField] private Sprite repairedTable;
        private void Awake()
        {
            Initialize();
            if (ProgressStorage.GetProgress(id) > 2)
            {
                backgroundController.ChangeSpriteWithoutFade(repairedTable);
            }
        }

        public override void Interact()
        {
            if (ProgressStorage.GetProgress(id) == 2)
            {
                backgroundController.ChangeSpriteWithFade(repairedTable);
                ProgressStorage.IncrementProgress(id);
            }
            else
            {
                var dialogueText = NPCProgressTracker.GetDialogue(id);
                Talk(dialogueText);
            }
        }

        public void Talk(DialogueText dialogueText)
        {
            if(MouseState.isInDialog) return;
            InitDialogueArea(dialogueText);
            dialogueController.DisplayNextParagraph(dialogueText);
            Debug.Log("Talking");
        }
        
        private void InitDialogueArea(DialogueText dialogueText)
        {
            skipDialoguesArea.dialogueText = dialogueText;
        }
    }
}
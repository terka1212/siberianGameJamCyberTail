using Game.Inventory;
using Game.UI;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public class Bulb : NPC, ITalkable
    {
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private SkipDialoguesArea skipDialoguesArea;
        [SerializeField] private BackgroundController backgroundController;
        [SerializeField] private Sprite repairedTable;
        [SerializeField] private Vector3 comingPlayerDirection;
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
            if (ProgressStorage.GetProgress(id) == 1)
            {
                Storage.AddItemById(3);
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
            Debug.Log("Talking to table");
        }
        
        private void InitDialogueArea(DialogueText dialogueText)
        {
            skipDialoguesArea.dialogueText = dialogueText;
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
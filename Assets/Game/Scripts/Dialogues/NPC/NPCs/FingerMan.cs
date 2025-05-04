using Game.Inventory;
using Game.UI;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public class FingerMan : NPC, ITalkable
    {
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private SkipDialoguesArea skipDialoguesArea;
        [SerializeField] private int NeedItemId;
        [SerializeField] private BackgroundController backgroundController;
        [SerializeField] private Sprite plug;
        [SerializeField] private Vector3 comingPlayerDirection;

        private void Awake()
        {
            Initialize();
        }

        public override void Interact()
        {
            if (ProgressStorage.GetProgress(id) == 2 && Storage.isItemInSlots(AllItems.GetItemById(NeedItemId)))
            {
                Storage.RemoveItem(AllItems.GetItemById(NeedItemId));
                ProgressStorage.IncrementProgress(id);
                backgroundController.ChangeSpriteWithFade(plug);
                return;
            }
            else if (ProgressStorage.GetProgress(id) == 2 && !Storage.isItemInSlots(AllItems.GetItemById(NeedItemId)))
            {
                Debug.Log("FirestFromChest: progress implies item, but is not");
                return;
            }

            var dialogueText = NPCProgressTracker.GetDialogue(id);
            Talk(dialogueText);
        }

        public void InitDialogueArea(DialogueText dialogueText)
        {
            skipDialoguesArea.dialogueText = dialogueText;
        }

        public void Talk(DialogueText dialogueText)
        {
            if (MouseState.isInDialog) return;
            InitDialogueArea(dialogueText);
            dialogueController.DisplayNextParagraph(dialogueText);
            Debug.Log("Talking");
        }

        public override Vector3 GetFrontPoint(LayerMask mask)
        {
            //11 - navigation layer
            if (Physics.Raycast(transform.position + comingPlayerDirection, Vector3.down, out RaycastHit hit, 30f,
                    mask))
            {
                return hit.point;
            }

            return transform.position + comingPlayerDirection;
        }
    }
}
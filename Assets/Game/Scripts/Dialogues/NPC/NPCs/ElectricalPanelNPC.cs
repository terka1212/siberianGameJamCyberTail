using Game.UI;
using UnityEngine;

namespace Game.Dialogues.NPC
{
    public class ElectricalPanelNPC : NPC, ITalkable
    {
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private SkipDialoguesArea skipDialoguesArea;
        [SerializeField] private BackgroundController backgroundController;
        [SerializeField] private Sprite closedSprite;
        [SerializeField] private Sprite openAndTurnOffSprite;
        [SerializeField] private Sprite openAndTurnOnSprite;
        [SerializeField] private Sprite openAndTurnOffAndBulbOffSprite;
        [SerializeField] private Sprite openAndTurnOnAndBulbOffSprite;
        [SerializeField] private Vector3 comingPlayerDirection;
        private void Awake()
        {
            Initialize();
            if (ProgressStorage.GetProgress(id) > 0)
            {
                ProgressStorage.electricPanelState = ElectricPanel.OpenAndTurnOn;
            }
            CheckSwitcher();
        }

        public override void Interact()
        {
            if (ProgressStorage.GetProgress(id) == 0)
            {
                var dialogueText = NPCProgressTracker.GetDialogue(id);
                Talk(dialogueText);
            }
            else
            {
                Switch();
                CheckSwitcher();
                ProgressStorage.IncrementProgress(id);
            }
        }

        private void Switch()
        {
            switch (ProgressStorage.electricPanelState)
            {
                case ElectricPanel.OpenAndTurnOn :
                    ProgressStorage.electricPanelState = ElectricPanel.OpenAndTurnOff;
                    break;
                case ElectricPanel.OpenAndTurnOff :
                    ProgressStorage.electricPanelState = ElectricPanel.OpenAndTurnOn;
                    break;
                case ElectricPanel.OpenAndTurnOffAndBulbOff :
                    ProgressStorage.electricPanelState = ElectricPanel.OpenAndTurnOnAndBulbOff;
                    break;
                case ElectricPanel.OpenAndTurnOnAndBulbOff :
                    ProgressStorage.electricPanelState = ElectricPanel.OpenAndTurnOffAndBulbOff;
                    break;
            }
        }

        private void CheckSwitcher()
        {
            switch (ProgressStorage.electricPanelState)
            {
                case ElectricPanel.Closed :
                    backgroundController.ChangeSpriteWithoutFade(closedSprite);
                    break;
                case ElectricPanel.OpenAndTurnOff :
                    backgroundController.ChangeSpriteWithoutFade(openAndTurnOffSprite);
                    break;
                case ElectricPanel.OpenAndTurnOn :
                    backgroundController.ChangeSpriteWithoutFade(openAndTurnOnSprite);
                    break;
                case ElectricPanel.OpenAndTurnOffAndBulbOff :
                    backgroundController.ChangeSpriteWithoutFade(openAndTurnOffAndBulbOffSprite);
                    break;
                case ElectricPanel.OpenAndTurnOnAndBulbOff :
                    backgroundController.ChangeSpriteWithoutFade(openAndTurnOnAndBulbOffSprite);
                    break;
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
using System.Collections.Generic;
using Game.Dialogues;
using Game.Dialogues.NPC;
using UnityEngine;

namespace Game.UI
{
    public class AIAssistant : MonoBehaviour, ITalkable, IInteractable
    {
        
        [SerializeField] private List<DialogueText> dialogueTexts;
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private SkipDialoguesArea skipDialoguesArea;

        public static AIAssistant Instance;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Instance = this;
                AIProgressStorage.SetProgressIndex(0);
                DontDestroyOnLoad(this);
            }
            
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

        public void Interact()
        {
            if(MouseState.isInDialog) return;
            var progressIndex = AIProgressStorage.ProgressIndex;
            if (progressIndex > -1 && progressIndex < dialogueTexts.Count)
            {
                var dialogueText = dialogueTexts[progressIndex];
                Talk(dialogueText);
                if (progressIndex == 0)
                {
                    AIProgressStorage.SetProgressIndex(progressIndex + 1);
                }
            }
            else
            {
                Debug.Log("Dialogue Index out of range or lower than 0. AIAssistant");
            }
        }
    }
}
using System;
using Game.Data;
using Game.Dialogues.NPC;
using Game.Events;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Game.Dialogues
{
    public class DialoguePresenter : IStartable, IDisposable
    {
        private DialogueDistributor _dialogueDistributor;
        private DialogueService _dialogueService;
        private EventManager _eventManager;
        
        private InputAction _clickAction;
        
        private DialogueText _cachedDialogueText;

        [Inject]
        public DialoguePresenter(DialogueDistributor dialogueDistributor, EventManager eventManager)
        {
            _dialogueDistributor = dialogueDistributor;
            _eventManager = eventManager;
        }
        
        public void Start()
        {
            _clickAction = InputSystem.actions.FindAction("Click");
            _eventManager.OnEndDialogue += ClearCash;
            _clickAction.performed += TrySkipOrNextParagraph;
        }

        public void Dispose()
        {
            _eventManager.OnEndDialogue -= ClearCash;
            _clickAction.performed -= TrySkipOrNextParagraph;
        }

        public void StartDialogue(long npcId)
        {
            _cachedDialogueText = _dialogueDistributor.GetCurrentDialogueByNpcId(npcId);
            _dialogueService.DisplayNextParagraph(_cachedDialogueText);
        }

        private void TrySkipOrNextParagraph(InputAction.CallbackContext context)
        {
            if (!_dialogueService.IsInDialog()) return;
            _dialogueService.DisplayNextParagraph(_cachedDialogueText);
        }
        
        private void ClearCash()
        {
            _cachedDialogueText = null;
        }
    }
}
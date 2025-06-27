using System;
using Game.Data;
using Game.Events;
using Game.Infrastructure;
using UnityEngine.InputSystem;
using VContainer;

namespace Game.Dialogues
{
    public class DialoguePresenter : IScopedStartable, IDisposable
    {
        private readonly DialogueDistributor _dialogueDistributor;
        private readonly EventManager _eventManager;
        private readonly ScopedLifecycleManager _scopedLifecycleManager;
        private readonly DialogueService _dialogueService;

        private InputAction _clickAction;
        private DialogueText _cachedDialogueText;

        [Inject]
        public DialoguePresenter(DialogueDistributor dialogueDistributor, EventManager eventManager,
            ScopedLifecycleManager scopedLifecycleManager, DialogueService dialogueService)
        {
            _dialogueDistributor = dialogueDistributor;
            _eventManager = eventManager;
            _scopedLifecycleManager = scopedLifecycleManager;
            _scopedLifecycleManager.Register(this);
            _dialogueService = dialogueService;
        }

        public void ScopedStart()
        {
            _clickAction = InputSystem.actions.FindAction("Click");
            _eventManager.OnEndDialogue += ClearCash;
            _clickAction.performed += TrySkipOrNextParagraph;
        }

        public void Dispose()
        {
            _eventManager.OnEndDialogue -= ClearCash;
            _clickAction.performed -= TrySkipOrNextParagraph;
            _scopedLifecycleManager.Unregister(this);
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
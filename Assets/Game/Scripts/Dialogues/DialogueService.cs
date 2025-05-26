using System.Collections;
using Game.Data;
using Game.Events;
using Game.Navigation;
using Game.Utils;
using VContainer;

namespace Game.Dialogues
{
    public class DialogueService
    {
        private DialogueView _dialogueView;
        private DialogueData _dialogueData;
        private CoroutineHandler _coroutineHandler;
        private PointAndClickService _pointAndClickService;
        private EventManager _eventManager;

        [Inject]
        public DialogueService(DialogueView dialogueView, DialogueData dialogueData, CoroutineHandler coroutineHandler,
            PointAndClickService pointAndClickService, EventManager eventManager)
        {
            _dialogueView = dialogueView;
            _dialogueData = dialogueData;
            _coroutineHandler = coroutineHandler;
            _pointAndClickService = pointAndClickService;
            _eventManager = eventManager;
        }

        public void DisplayNextParagraph(DialogueText dialogueText)
        {
            if (_dialogueData.paragraphs.Count == 0)
            {
                if (!_dialogueData.conversationEnded)
                {
                    StartConversation(dialogueText);
                }
                else if (_dialogueData.conversationEnded && !_dialogueData.isTyping)
                {
                    EndConversation();
                    return;
                }
            }

            //if there is something in queue
            if (!_dialogueData.isTyping)
            {
                _dialogueData.currentParagraph = _dialogueData.paragraphs.Dequeue();

                _dialogueData.currentTypingCoroutine =
                    _coroutineHandler.StartCoroutine(TypeDialogueText(_dialogueData.currentParagraph.text));
            }
            else
            {
                FinishParagraphEarly();
            }

            //update Conversation text
            _dialogueView.SetNPCNameAndImage(_dialogueData.currentParagraph.speakerName,
                _dialogueData.currentParagraph.SpeakerSprite);

            if (_dialogueData.paragraphs.Count == 0)
            {
                _dialogueData.ConversationEnded();
            }
        }

        public bool IsInDialog() => _dialogueView.gameObject.activeSelf;

        private void StartConversation(DialogueText dialogueText)
        {
            if (!_dialogueView.gameObject.activeSelf)
            {
                _dialogueView.ActivateDialogueView();
                _pointAndClickService.Block();
                _eventManager.InvokeOnDialogueStarted();
            }

            for (int i = 0; i < dialogueText.paragraphs.Length; i++)
            {
                _dialogueData.paragraphs.Enqueue(dialogueText.paragraphs[i]);
            }
        }

        private void EndConversation()
        {
            _dialogueData.PrepareForNewConversation();

            if (_dialogueView.gameObject.activeSelf)
            {
                _dialogueView.DeactivateDialogueView();
                _pointAndClickService.Unblock();
                _eventManager.InvokeOnEndDialogue();
            }
        }

        private IEnumerator TypeDialogueText(string text)
        {
            _dialogueData.StartTyping();
            int maxVisibleChars = 0;

            var npcDialogueText = _dialogueView.GetNPCDialogueText();
            npcDialogueText.text = text;
            npcDialogueText.maxVisibleCharacters = maxVisibleChars;

            foreach (char c in text.ToCharArray())
            {
                maxVisibleChars++;
                npcDialogueText.maxVisibleCharacters = maxVisibleChars;

                yield return _dialogueData.cachedWait;
            }

            _dialogueData.StopTyping();
        }

        private void FinishParagraphEarly()
        {
            _coroutineHandler.StopCoroutine(_dialogueData.currentTypingCoroutine);
            _dialogueView.GetNPCDialogueText().maxVisibleCharacters = _dialogueData.currentParagraph.text.Length;
            _dialogueData.StopTyping();
        }
    }
}
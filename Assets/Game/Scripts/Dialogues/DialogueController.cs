using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private SkipDialoguesArea skipDialoguesArea;
        [SerializeField] private TextMeshProUGUI NPCNameText;
        [SerializeField] private TextMeshProUGUI NPCDialogueText;
        [SerializeField] private Image NPCAvatarImage;
        [SerializeField] private float typeSpeed = 10;

        private Queue<Paragraph> paragraphs = new Queue<Paragraph>();

        private bool conversationEnded;
        private bool isTyping;

        private Paragraph paragraph;

        private Coroutine typeDialogueCoroutine;

        private const string HTML_ALPHA = "<color=#00000000>";
        private const float MAX_TYPE_TIME = 0.1f;

        private WaitForSeconds typeDialogueCachedWait;

        private void Start()
        {
            typeDialogueCachedWait = new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        public void DisplayNextParagraph(DialogueText dialogueText)
        {
            if (paragraphs.Count == 0)
            {
                if (!conversationEnded)
                {
                    StartConversation(dialogueText);
                }
                else if (conversationEnded && !isTyping)
                {
                    EndConversation();
                    return;
                }
            }

            //if there is something in queue
            if (!isTyping)
            {
                paragraph = paragraphs.Dequeue();
                typeDialogueCoroutine = StartCoroutine(TypeDialogueText(paragraph.text));
            }
            else
            {
                FinishParagraphEarly();
            }


            //update Conversation text
            NPCNameText.text = paragraph.speakerName;
            //NPCDialogueText.text = paragraph.text;
            NPCAvatarImage.sprite = paragraph.SpeakerSprite;
            if (paragraphs.Count == 0)
            {
                conversationEnded = true;
            }
        }

        private void StartConversation(DialogueText dialogueText)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                MouseState.isInDialog = true;
            }

            for (int i = 0; i < dialogueText.paragraphs.Length; i++)
            {
                paragraphs.Enqueue(dialogueText.paragraphs[i]);
            }
        }

        private void EndConversation()
        {
            conversationEnded = false;

            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                MouseState.isInDialog = false;
                skipDialoguesArea.dialogueText = null;
            }
        }

        private IEnumerator TypeDialogueText(string text)
        {
            isTyping = true;
            int maxVisibleChars = 0;

            NPCDialogueText.text = text;
            NPCDialogueText.maxVisibleCharacters = maxVisibleChars;

            foreach (char c in text.ToCharArray())
            {
                maxVisibleChars++;
                NPCDialogueText.maxVisibleCharacters = maxVisibleChars;

                yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
            }

            isTyping = false;
        }

        private void FinishParagraphEarly()
        {
            StopCoroutine(typeDialogueCoroutine);

            NPCDialogueText.maxVisibleCharacters = paragraph.text.Length;

            isTyping = false;
        }
    }
}
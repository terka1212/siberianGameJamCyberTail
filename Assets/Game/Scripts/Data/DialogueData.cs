using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public class DialogueData
    {
        public readonly float maxTypeTime = 0.1f;
        public readonly float typeSpeed = 10;
        public Queue<Paragraph> paragraphs;
        public bool conversationEnded { get; private set; }
        public bool isTyping { get; private set; }

        public readonly WaitForSeconds cachedWait;
        public Coroutine currentTypingCoroutine;
        public Paragraph currentParagraph;

        public DialogueData(float typeSpeed, float maxTypeTime)
        {
            this.maxTypeTime = maxTypeTime;
            this.typeSpeed = typeSpeed;
            paragraphs = new Queue<Paragraph>();
            if (typeSpeed != 0)
                cachedWait = new WaitForSeconds(maxTypeTime / typeSpeed);
            else
                cachedWait = new WaitForSeconds(1f);
        }

        public void ConversationEnded()
        {
            conversationEnded = true;
        }

        public void PrepareForNewConversation()
        {
            conversationEnded = true;
        }

        public void StartTyping()
        {
            isTyping = true;
        }

        public void StopTyping()
        {
            isTyping = false;
        }
    }
}
using System;
using UnityEngine;

namespace Game.Dialogues
{
    [CreateAssetMenu(menuName = "Utilities/Dialogue System/Dialogue Text")]
    public class DialogueText : ScriptableObject
    {
        public Paragraph[] paragraphs;
    }

    [Serializable]
    public class Paragraph
    {
        public Sprite SpeakerSprite;
        public string speakerName;
        [TextArea(5,10)]
        public string text;
    }
}
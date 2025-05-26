using System;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "NewDialogueText", menuName = "Utilities/Dialogue System/Dialogue Text")]
    public class DialogueText : ScriptableObject
    {
        public long id;
        public Paragraph[] paragraphs;

        protected bool Equals(DialogueText other)
        {
            return base.Equals(other) && id == other.id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DialogueText)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), id);
        }
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
using System.Collections.Generic;

namespace Game.Dialogues.NPC
{
    public static class NPCProgressTracker
    {
        private static Dictionary<int, List<DialogueText>> dialogues = new Dictionary<int, List<DialogueText>>();

        private static bool isInitialized = false;
        public static void InitDialogue(int npc, List<DialogueText> dialogueTexts)
        {
            if(dialogues.ContainsKey(npc)) return;
            dialogues[npc] = dialogueTexts;
            isInitialized = true;
        }

        public static DialogueText GetDialogue(int npc)
        {
            var progress = ProgressStorage.GetProgress(npc);
            return dialogues[npc][progress];
        }
    }
}
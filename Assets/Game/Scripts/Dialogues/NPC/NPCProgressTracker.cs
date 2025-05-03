using System.Collections.Generic;

namespace Game.Dialogues.NPC
{
    public static class NPCProgressTracker
    {
        private static Dictionary<NPC, List<DialogueText>> dialogues = new Dictionary<NPC, List<DialogueText>>();

        public static void InitDialogue(NPC npc, List<DialogueText> dialogueTexts)
        {
            dialogues[npc] = dialogueTexts;
        }

        public static DialogueText GetDialogue(NPC npc)
        {
            var progress = ProgressStorage.GetProgress(npc.id);
            return dialogues[npc][progress];
        }
    }
}
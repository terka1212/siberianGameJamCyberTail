using System.Collections.Generic;

namespace Game.Dialogues.NPC
{
    public static class ProgressStorage
    {
        //npcID, progressInt
        private static Dictionary<int, int> progress = new Dictionary<int, int>();

        public static int GetProgress(int npcId)
        {
            return progress[npcId];
        }

        public static void IncrementProgress(int npcId)
        {
            progress[npcId]++;
        }

        public static void SetProgress(int npcId, int progressInt)
        {
            progress.Add(npcId, progressInt);
        }
    }
}
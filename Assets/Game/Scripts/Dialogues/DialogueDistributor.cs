using System.Collections.Generic;
using Game.Data;
using Game.Events;
using VContainer;

namespace Game.Dialogues
{
    public class DialogueDistributor
    {
        private Dictionary<long, Data.NPC> _npcs;
        private EventManager _eventManager;

        [Inject]
        public DialogueDistributor(EventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public void AddNPC(Data.NPC npc)
        {
            _npcs.Add(npc.id, npc);
        }

        public DialogueText GetCurrentDialogueByNpcId(long npcId)
        {
            if (!_npcs.TryGetValue(npcId, out Data.NPC npc))
            {
                _eventManager.InvokeOnNPCNotExisted(npcId);
                return null;
            }
            
            var npcText = npc.dialogues[npc.currentProgress];
            if (npcText != null) return npcText;
            
            _eventManager.InvokeOnNPCDialogueNull(npcId, npc.currentProgress);
            return null;
        }
    }
}
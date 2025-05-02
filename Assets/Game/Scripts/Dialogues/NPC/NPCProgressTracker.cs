using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Dialogues.NPC
{
    public class NPCProgressTracker : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<NPC, List<DialogueText>> dialogues;
        
        private static NPCProgressTracker _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public static void InitDialogue(NPC npc, List<DialogueText> dialogueTexts)
        {
            _instance.dialogues[npc] = dialogueTexts;
        }

        public static DialogueText GetDialogue(NPC npc)
        {
            var progress = ProgressStorage.GetProgress(npc.id);
            return _instance.dialogues[npc][progress];
        }
    }
}
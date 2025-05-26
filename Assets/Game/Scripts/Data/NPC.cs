using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "NewNPCInfo", menuName = "Utilities/Dialogue System/NPC")]
    public class NPC : ScriptableObject
    {
        [SerializeField] public long id;
        [SerializeField] public int currentProgress;
        [SerializeField] public List<DialogueText> dialogues;
    }
}
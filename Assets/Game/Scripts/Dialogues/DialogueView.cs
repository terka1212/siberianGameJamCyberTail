using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI NPCNameText;
        [SerializeField] private TextMeshProUGUI NPCDialogueText;
        [SerializeField] private Image NPCAvatarImage;

        public void SetNPCNameAndImage(string name, Sprite avatar)
        {
            NPCNameText.text = name;
            NPCAvatarImage.sprite = avatar;
        }

        public TextMeshProUGUI GetNPCDialogueText()
        {
            return NPCDialogueText;
        }

        public void DeactivateDialogueView()
        {
            gameObject.SetActive(false);
        }

        public void ActivateDialogueView()
        {
            gameObject.SetActive(true);
        }
    }
}
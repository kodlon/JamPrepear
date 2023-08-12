using UnityEngine;

namespace UI.DialogSystem
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Create/Dialog", order = 0)]
    public class DialogSO : ScriptableObject
    {
        public string characterName;
        [TextArea(0, 10)]
        public string dialogText;
    
        public Sprite characterSprite;
        public SpecialEventsEnum specialEvent;
    }
}
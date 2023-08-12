using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogSystem
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _characterNameText;
        [SerializeField] private TMP_Text _dialogText;
        [SerializeField] private Image _characterSpriteRenderer;
        [SerializeField] private Button _dialogButton;
        
        [SerializeField] private Sprite _standardCharacterIcon;
        [SerializeField] private Image _hintIcon;
        [SerializeField] private EventController _eventController;
    
        private List<DialogSO> _dialogList;
        
        public event Action OnDialogEnd;
    
        private int _dialogIndex;
        
        public void SetupNewDialog(List<DialogSO> dialogList)
        {
            _dialogList = dialogList;
            
            _characterNameText.text = _dialogList[_dialogIndex].characterName;
            _dialogText.text = _dialogList[_dialogIndex].dialogText;
            _characterSpriteRenderer.sprite = _dialogList[_dialogIndex].characterSprite;
            _eventController.HandleEvent(_dialogList[_dialogIndex].specialEvent);
            
            _dialogButton.interactable = true;
            _hintIcon.gameObject.SetActive(true);
        }

        private void Start()
        {
            _dialogButton.onClick.AddListener(NextDialog);
        }

        private void OnDestroy()
        {
            _dialogButton.onClick.RemoveListener(NextDialog);
            EndDialogAfterAction();
        }

        private void NextDialog()
        {
            _dialogIndex++;

            if (_dialogList.Count <= _dialogIndex)
            {
                EndDialogAfterAction();
                return;
            }
        
            _characterNameText.text = _dialogList[_dialogIndex].characterName;
            _dialogText.text = _dialogList[_dialogIndex].dialogText;
            _characterSpriteRenderer.sprite = _dialogList[_dialogIndex].characterSprite;
            _eventController.HandleEvent(_dialogList[_dialogIndex].specialEvent);
        }

        private void EndDialogAfterAction()
        {
            _dialogButton.interactable = false;
            _characterNameText.text = "-";
            _dialogText.text = "-";
            _characterSpriteRenderer.sprite = _standardCharacterIcon;
            _dialogIndex = 0;
            _hintIcon.gameObject.SetActive(false);

            OnDialogEnd?.Invoke();
        }
    }
}
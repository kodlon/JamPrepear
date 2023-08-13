using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DialogSystem
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _characterNameText;
        [SerializeField] private TMP_Text _dialogText;
        [SerializeField] private Image _characterSpriteRenderer;
        [SerializeField] private Button _dialogButton;
        [SerializeField] private float _dialogSpeed;
        
        [SerializeField] private Sprite _standardCharacterIcon;
        [SerializeField] private Image _hintIcon;
        [SerializeField] private EventController _eventController;
        
        public event Action OnDialogEnd;
    
        private Coroutine _dialogProcessCoroutine;
        private List<DialogSO> _dialogList;
    
        private int _dialogIndex = -1;
        
        public void SetupNewDialog(List<DialogSO> dialogList)
        {
            _dialogList = dialogList;
            _dialogButton.interactable = true;
            _hintIcon.gameObject.SetActive(true);
            
            NextDialog();
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
            if (_dialogProcessCoroutine != null)
                StopCoroutine(_dialogProcessCoroutine);
            
            _dialogProcessCoroutine = StartCoroutine(DialogProcess());
        }

        private IEnumerator DialogProcess()
        {
            _dialogButton.interactable = false;
            
            _dialogIndex++;

            if (_dialogList.Count <= _dialogIndex)
            {
                _eventController.HandleEvent(_dialogList[_dialogIndex - 1].specialEvent);
                EndDialogAfterAction();
                yield break;
            }

            _characterNameText.text = _dialogList[_dialogIndex].characterName;
            _characterSpriteRenderer.sprite = _dialogList[_dialogIndex].characterSprite;

            var fullText = _dialogList[_dialogIndex].dialogText;
            _dialogText.text = "";

            foreach (var letter in fullText)
            {
                _dialogText.text += letter;
                yield return new WaitForSeconds(_dialogSpeed);
            }
            
            _dialogButton.interactable = true;

            if (_dialogList[_dialogIndex].specialEvent == SpecialEventsEnum.ReceiveWeapon)
                _eventController.HandleEvent(_dialogList[_dialogIndex].specialEvent);
        }

        private void EndDialogAfterAction()
        {
            _dialogButton.interactable = false;
            _characterNameText.text = "-";
            _dialogText.text = "-";
            _characterSpriteRenderer.sprite = _standardCharacterIcon;
            _dialogIndex = -1;
            _hintIcon.gameObject.SetActive(false);

            OnDialogEnd?.Invoke();
        }
    }
}
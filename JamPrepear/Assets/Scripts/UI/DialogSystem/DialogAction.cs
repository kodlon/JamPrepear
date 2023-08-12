using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace UI.DialogSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogAction : MonoBehaviour
    {
        [SerializeField] DialogView _dialogView;
        [SerializeField] private List<DialogSO> _dialogList;
        
        private bool _isDialogEnded;

        private void Start()
        {
            if (_dialogView == null)
                Debug.LogError($"DialogView is null in DialogAction: {name}");
        }

        //TODO: Add enemies action to false, via factory in future
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDialogEnded || !other.TryGetComponent<PlayerController>(out var playerController)) 
                return;
            
            _dialogView.SetupNewDialog(_dialogList);
            playerController.SetActionAllowed(false);
            _dialogView.OnDialogEnd += OnDialogEnd;

            void OnDialogEnd()
            {
                playerController.SetActionAllowed(true);
                _dialogView.OnDialogEnd -= OnDialogEnd;
                
                _isDialogEnded = true;
            }
        }
    }
}
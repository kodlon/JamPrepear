using DialogSystem;
using Entities.Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogView _dialogView;
    [SerializeField] private PlayerController _playerController;
    
    private void Awake()
    {
        Application.targetFrameRate = 120;
        
        _dialogView.OnDialogEnd += OnDialogEnd;
        _playerController.OnPlayerDeath += OnPlayerDeath;
    }
    
    private void OnDestroy()
    {
        _dialogView.OnDialogEnd -= OnDialogEnd;
        _playerController.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnDialogEnd()
    {
        Debug.LogError("Dialog ended");
    }
    
    private void OnPlayerDeath()
    {
        Debug.LogError("Game Over");
    }
}
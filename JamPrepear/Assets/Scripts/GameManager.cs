using Entities.Player;
using UI.DialogSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogView _dialogView;
    [SerializeField] private PlayerController _playerController;
    
    private void Awake()
    {
        Application.targetFrameRate = 120;
        
        _playerController.OnPlayerDeath += OnPlayerDeath;
    }
    
    private void OnDestroy()
    {
        _playerController.OnPlayerDeath -= OnPlayerDeath;
    }
    
    private void OnPlayerDeath()
    {
        Debug.LogError("Game Over");
    }
}
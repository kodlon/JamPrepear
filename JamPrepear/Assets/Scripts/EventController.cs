using System;
using Entities.Enemies;
using Entities.Player;
using UI.DialogSystem;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private DialogEvent[] _dialogEvent;
    
    private SpecialEventsEnum _currentSpecialEvent;

    public void HandleEvent(SpecialEventsEnum specialEvent)
    {
        switch (specialEvent)
        {
            case SpecialEventsEnum.None:
                break;
            case SpecialEventsEnum.ReceiveWeapon:
                _currentSpecialEvent = SpecialEventsEnum.ReceiveWeapon;
                _playerController.IsWeaponReceived = true;
                break;
            case SpecialEventsEnum.FirstWave:
                _currentSpecialEvent = SpecialEventsEnum.FirstWave;
                
                for (var i = 0; i < 1; i++)
                {
                    _enemySpawner.SpawnBowEnemy(_enemySpawner.transform.position, Quaternion.identity);
                }
                break;
            case SpecialEventsEnum.SecondWave:
                _currentSpecialEvent = SpecialEventsEnum.SecondWave;
                
                for (var i = 0; i < 2; i++)
                {
                    _enemySpawner.SpawnBowEnemy(_enemySpawner.transform.position, Quaternion.identity);
                }
                break;
            case SpecialEventsEnum.ThirdWave:
                _currentSpecialEvent = SpecialEventsEnum.ThirdWave;
                break;
            case SpecialEventsEnum.BossWave:
                _currentSpecialEvent = SpecialEventsEnum.BossWave;

                Debug.LogError("WIN!!!");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(specialEvent), specialEvent, null);
        }
    }
    
    private void Start()
    {
        _enemySpawner.OnAllEnemiesDied += OnAllEnemiesDied;
    }

    private void OnDestroy()
    {
        _enemySpawner.OnAllEnemiesDied -= OnAllEnemiesDied;
    }

    private void OnAllEnemiesDied()
    {
        foreach (var dialogEvent in _dialogEvent)
        {
            if (dialogEvent.SpecialEvent == _currentSpecialEvent)
            {
                dialogEvent.DialogAction.gameObject.SetActive(true);
                break;
            }
        }
    }
    
    [Serializable]
    private class DialogEvent
    {
        public SpecialEventsEnum SpecialEvent;
        public DialogAction DialogAction;
    }
}
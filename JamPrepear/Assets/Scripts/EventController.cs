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
    
    [SerializeField] private SpriteRenderer _bossThrone;
    [SerializeField] private Sprite _emptyThroneSprite;
    
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
                
                for (var i = 0; i < 2; i++)
                {
                    _enemySpawner.SpawnSwordEnemy();
                }
                
                _enemySpawner.SpawnBowEnemy();

                break;
            case SpecialEventsEnum.SecondWave:
                _currentSpecialEvent = SpecialEventsEnum.SecondWave;

                for (var i = 0; i < 1; i++)
                {
                    _enemySpawner.SpawnSwordEnemy();
                }             
                
                for (var i = 0; i < 2; i++)
                {
                    _enemySpawner.SpawnBowEnemy();
                }
                
                break;
            case SpecialEventsEnum.ThirdWave:
                _currentSpecialEvent = SpecialEventsEnum.ThirdWave;

                for (var i = 0; i < 4; i++)
                {
                    _enemySpawner.SpawnSwordEnemy();
                    _enemySpawner.SpawnBowEnemy();
                }
                
                break;
            case SpecialEventsEnum.BossWave:
                _currentSpecialEvent = SpecialEventsEnum.BossWave;
                _bossThrone.sprite = _emptyThroneSprite;
                
                
                for (var i = 0; i < 2; i++)
                {
                    _enemySpawner.SpawnSwordEnemy();
                    _enemySpawner.SpawnBowEnemy();
                }
                
                _enemySpawner.SpawnBossEnemy();
                
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
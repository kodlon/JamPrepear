using System;
using Entities.Enemies;
using Entities.Player;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private EnemySpawner _enemySpawner;
    
    public void HandleEvent(SpecialEventsEnum specialEvent)
    {
        switch (specialEvent)
        {
            case SpecialEventsEnum.None:
                break;
            case SpecialEventsEnum.ReceiveWeapon:
                _playerController.IsWeaponReceived = true;
                break;
            case SpecialEventsEnum.FirstWave:
                for (var i = 0; i < 1; i++)
                {
                    _enemySpawner.SpawnBowEnemy(_enemySpawner.transform.position, Quaternion.identity);
                }
                break;
            case SpecialEventsEnum.SecondWave:
                break;
            case SpecialEventsEnum.ThirdWave:
                break;
            case SpecialEventsEnum.BossWave:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(specialEvent), specialEvent, null);
        }
    }
}
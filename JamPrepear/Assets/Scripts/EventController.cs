using System;
using Entities.Player;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    
    public void HandleEvent(SpecialEventsEnum specialEvent)
    {
        switch (specialEvent)
        {
            case SpecialEventsEnum.ReceiveWeapon:
                _playerController.IsWeaponReceived = true;
                break;
            case SpecialEventsEnum.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(specialEvent), specialEvent, null);
        }
    }
}
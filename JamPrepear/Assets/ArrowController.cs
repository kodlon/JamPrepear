using Entities.Enemies;
using Entities.Interfaces;
using Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            playerController.TakeDamage(1);
            Debug.Log("Стріла їбало: " + other.gameObject.name);
            Destroy(gameObject);
        }
        
    }
}

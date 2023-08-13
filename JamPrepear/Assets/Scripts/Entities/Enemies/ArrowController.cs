using Entities.Player;
using UnityEngine;

namespace Entities.Enemies
{
    public class ArrowController : MonoBehaviour
    {
       
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerController>(out var playerController)) 
                return;
            
            playerController.TakeDamage(1);
            Debug.Log("Стріла їбало: " + other.name);
            Destroy(gameObject);
        }
    }
}

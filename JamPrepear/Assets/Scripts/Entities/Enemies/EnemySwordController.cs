using Entities.Interfaces;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemySwordController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageable)) 
                return;
            
            damageable.TakeDamage(1);
            Debug.Log("Хуяк по їбалу: " + other.gameObject.name);
        }
    }
}
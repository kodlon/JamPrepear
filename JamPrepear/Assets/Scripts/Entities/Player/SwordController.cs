using Entities.Interfaces;
using UnityEngine;

namespace Entities.Player
{
    public class SwordController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(1);
                Debug.Log("Хуяк по їбалу: " + other.gameObject.name);
            }
        }
    }
}
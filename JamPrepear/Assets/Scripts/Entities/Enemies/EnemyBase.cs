using System;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health = 1;
        public Transform Target { get; set; }
        
        public event Action<EnemyBase> OnDie;

        public int Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, int.MaxValue);
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;
            
            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
            OnDie?.Invoke(this);
        }
    }
}
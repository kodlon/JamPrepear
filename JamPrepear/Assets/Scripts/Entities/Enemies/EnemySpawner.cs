using System;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private ChasingEnemy _chasingEnemyPrefab;
        [SerializeField] private BowEnemy _bowEnemyPrefab;
        [SerializeField] private PlayerController _playerController;
        
        public event Action OnAllEnemiesDied;
        
        private int _enemiesCount;
        
        public void SpawnChasingEnemy(Vector3 position, Quaternion rotation)
        {
            var enemy = Instantiate(_chasingEnemyPrefab, position, rotation);
            enemy.Target = _playerController.transform;
            enemy.OnDie += EnemiesDied;
            
            _enemiesCount++;
        }
        
        public void SpawnBowEnemy(Vector3 position, Quaternion rotation)
        {
            var enemy = Instantiate(_bowEnemyPrefab, position, rotation);
            enemy.Target = _playerController.transform;
            enemy.OnDie += EnemiesDied;
            
            _enemiesCount++;
        }
        
        private void EnemiesDied(EnemyBase enemy)
        {
            _enemiesCount--;
            enemy.OnDie -= EnemiesDied;

            if (_enemiesCount != 0) 
                return;
            
            _enemiesCount = 0;
            OnAllEnemiesDied?.Invoke();
        }
    }
}
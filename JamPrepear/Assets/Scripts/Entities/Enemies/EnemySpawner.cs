using System;
using Entities.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Entities.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SwordEnemy _swordEnemyPrefab;
        [SerializeField] private SwordEnemy _bossEnemyPrefab;
        [SerializeField] private BowEnemy _bowEnemyPrefab;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform[] _spawnPoints;
        
        public event Action OnAllEnemiesDied;
        
        private int _enemiesCount;

        public void SpawnSwordEnemy()
        {
            var enemy = Instantiate(_swordEnemyPrefab, _spawnPoints[Random.Range(1, _spawnPoints.Length)].position, Quaternion.identity);
            enemy.Target = _playerController.transform;
            enemy.OnDie += EnemiesDied;
            
            _enemiesCount++;
        }
        
        public void SpawnBowEnemy()
        {
            var enemy = Instantiate(_bowEnemyPrefab, _spawnPoints[Random.Range(1, _spawnPoints.Length)].position, Quaternion.identity);
            enemy.Target = _playerController.transform;
            enemy.OnDie += EnemiesDied;
            
            _enemiesCount++;
        }        
        
        public void SpawnBossEnemy()
        {
            var enemy = Instantiate(_bossEnemyPrefab, _spawnPoints[0].position,Quaternion.identity);
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
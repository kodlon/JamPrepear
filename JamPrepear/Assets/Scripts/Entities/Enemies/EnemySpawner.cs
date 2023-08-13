using Entities.Player;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private ChasingEnemy _chasingEnemyPrefab;
        [SerializeField] private BowEnemy _bowEnemyPrefab;
        [SerializeField] private PlayerController _playerController;
        
        public void SpawnChasingEnemy(Vector3 position, Quaternion rotation)
        {
            var enemy = Instantiate(_chasingEnemyPrefab, position, rotation);
            enemy.Target = _playerController.transform;
        }
        
        public void SpawnBowEnemy(Vector3 position, Quaternion rotation)
        {
            var enemy = Instantiate(_bowEnemyPrefab, position, rotation);
            enemy.Target = _playerController.transform;
        }
    }
}
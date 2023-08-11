using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    public class ChasingEnemy : EnemyBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed = 5.0f;
        
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateUpAxis = false;
        }

        private void FixedUpdate()
        {
            if (_target == null)
                return;
            
            RotateTowardsPlayer();
            _navMeshAgent.SetDestination(_target.position);
        }
        
        private void RotateTowardsPlayer()
        {
            Vector2 direction = _target.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
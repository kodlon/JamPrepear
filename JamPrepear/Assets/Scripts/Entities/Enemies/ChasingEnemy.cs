using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    public class ChasingEnemy : EnemyBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed = 5.0f;
        [SerializeField] private float _attackRange = 5.0f;
        
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

            if (Vector3.Distance(_target.position, transform.position) <= _attackRange)
            {
                Attack();
                _navMeshAgent.isStopped = true;
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_target.position);
            }
        }
        
        private void RotateTowardsPlayer()
        {
            Vector2 direction = _target.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
        private void Attack()
        {
            Debug.Log("Хуяк гравцю по їбалу, а гравець в нас: " + _target.name);
        }
    }
}
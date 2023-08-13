using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    public class BowEnemy : EnemyBase
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private ArrowController _arrowPrefab;
        
        [SerializeField] private float _rotationSpeed = 5.0f;
        [SerializeField] private float _attackRange = 5.0f;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float attackCooldown = 0.5f;

        private NavMeshAgent _navMeshAgent;
        private float _nextAttackTime; 
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateUpAxis = false;
        }

        private void FixedUpdate()
        {
            if (Target == null)
                return;

            RotateTowardsPlayer();

            if (Vector3.Distance(Target.position, transform.position) <= _attackRange)
            {
                if (Time.time >= _nextAttackTime)
                {
                    Attack();
                    _nextAttackTime = Time.time + attackCooldown;
                }
                _navMeshAgent.isStopped = true;
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(Target.position);
            }
        }
        
        private void RotateTowardsPlayer()
        {
            Vector2 direction = Target.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
        
        private void Attack()
        {
            var newProjectile = Instantiate(_arrowPrefab, firePoint.position, Quaternion.Euler(0f, 0f, firePoint.rotation.eulerAngles.z - 90f));
            var rb = newProjectile.GetComponent<Rigidbody2D>();
            
            rb.velocity = firePoint.right * projectileSpeed;
        }
    }
}
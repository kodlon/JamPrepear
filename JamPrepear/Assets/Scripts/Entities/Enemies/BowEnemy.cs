using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    public class BowEnemy : EnemyBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed = 5.0f;
        [SerializeField] private float _attackRange = 5.0f;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float projectileSpeed = 10f; // Швидкість проектайлу
        [SerializeField] private float attackCooldown = 0.5f; // Перерва між атаками

        private float nextAttackTime = 0f; // Час наступної атаки
        private NavMeshAgent _navMeshAgent;
        public GameObject arrowPrefab; // Префаб проектайлу    
        


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
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackCooldown;
                }
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
            Debug.Log("Атака");
            GameObject newProjectile = Instantiate(arrowPrefab, firePoint.position, Quaternion.Euler(0f, 0f, firePoint.rotation.eulerAngles.z - 90f));

            // Отримати компонент Rigidbody2D проектайлу
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

            // Застосувати силу для руху проектайлу
            rb.velocity = firePoint.right * projectileSpeed;
        }
    }
}
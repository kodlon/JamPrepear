using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    public class SwordEnemy : EnemyBase
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private ArrowController _arrowPrefab;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private float _rotationSpeed = 5.0f;
        [SerializeField] private float _attackRange = 5.0f;
        [SerializeField] private float _projectileSpeed = 10f;
        [SerializeField] private float _attackCooldown = 0.5f;
        [SerializeField] private float _beforeAttackDuration = 0.0f;
        
        private NavMeshAgent _navMeshAgent;
        private Coroutine _attackCoroutine;

        private float _nextAttackTime; 
        private bool _isAttacking;

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
                if (_isAttacking)
                    return;
 
                if (_attackCoroutine != null)
                    StopCoroutine(_attackCoroutine);
                
                _attackCoroutine = StartCoroutine(Attack());
                
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
        
        private IEnumerator Attack()
        {
            _isAttacking = true;
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(_beforeAttackDuration);

            var newProjectile = Instantiate(_arrowPrefab, firePoint.position, Quaternion.Euler(0f, 0f, firePoint.rotation.eulerAngles.z - 90f));
            var rb = newProjectile.GetComponent<Rigidbody2D>();
            
            rb.velocity = firePoint.right * _projectileSpeed;

            Destroy(newProjectile.gameObject, .5f);
            yield return new WaitForSeconds(_attackCooldown);

            _isAttacking = false;
        }
    }
}
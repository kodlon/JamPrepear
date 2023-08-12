using System;
using System.Collections;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.Enemies
{
    public class AlebardEnemy : EnemyBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private SpriteRenderer _elebardPreparingSprite;
        [SerializeField] private Collider2D _alebardCollider;

        [SerializeField] private float _rotationSpeed = 5.0f;
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _attackPreparing = 0.5f;
        [SerializeField] private float _attackDuration = 1.0f;
        [SerializeField] private float _attackCooldown = 0.3f;

        private UnityEngine.AI.NavMeshAgent _navMeshAgent;
        private float _nextAttackTime;
        private float _preparationTime;
        private Coroutine _attackCoroutine;
        private Coroutine _preparationCoroutine;

        private void Awake()
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _navMeshAgent.updateUpAxis = false;
        }

        private void FixedUpdate()
        {
            if (_target == null)
                return;

            RotateTowardsPlayer();

            if (Vector3.Distance(_target.position, transform.position) <= _attackRange)
            {
                if (Time.time >= _nextAttackTime)
                {
                    _nextAttackTime = Time.time + _attackPreparing + _attackDuration + _attackCooldown;
                    if (Time.time >= _preparationTime)
                    {
                        Attack();
                    }
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
        
        private void Prepare()
        {
            Debug.Log($"Preparing {name}");

            _elebardPreparingSprite.enabled = true;

            if (_preparationCoroutine != null)
                StopCoroutine(_preparationCoroutine);

            //StartCoroutine(AlebardePreparation());
        }
        private IEnumerable AlebardePreparation()
        {
            _alebardCollider.gameObject.SetActive(true);
            _alebardCollider.enabled = true;
            yield return new WaitForSeconds(_attackDuration);
            _alebardCollider.enabled = false;
            _alebardCollider.gameObject.SetActive(false);
        }

        private void Attack()
        {
            Debug.Log($"Attack {name}");

            _alebardCollider.enabled = true;

            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);

            StartCoroutine(SwordAttack());
        }
        private IEnumerator SwordAttack()
        {
            //_animator.SetTrigger("Attack");
            _alebardCollider.gameObject.SetActive(true);
            _alebardCollider.enabled = true;
            yield return new WaitForSeconds(_attackDuration);
            _alebardCollider.enabled = false;
            _alebardCollider.gameObject.SetActive(false);
        }
    }
}
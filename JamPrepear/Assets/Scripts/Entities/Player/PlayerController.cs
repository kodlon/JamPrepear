using System;
using System.Collections;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IDamageable, IActionControl
    {
        [SerializeField] private float _movingSpeed = 5.0f;
        [SerializeField] private float _rotationSpeed = 200.0f;
        [SerializeField] private float _attackDuration = 0.1f;
        [SerializeField] private float _beforeAttackDuration = 0.1f;
        [SerializeField] private int _healthPoints = 3;
        
        [SerializeField] private Collider2D _swordCollider;
        [SerializeField] private Animator _animator;
        
        public event Action<int> OnHealthChanged;

        public int Health
        {
            get => _healthPoints;
            set
            {
                if (_healthPoints != value)
                {
                    _healthPoints = Mathf.Clamp(value, 0, int.MaxValue);
                    OnHealthChanged?.Invoke(_healthPoints);
                }
            }
        }
        
        public event Action OnPlayerDeath;

        private Rigidbody2D _rigidbody2D;
        private Camera _mainCamera;
        private Transform _playerTransform;
        private Coroutine _attackCoroutine;
        
        private bool _isActionAllowed = true;
        private bool _isWeaponReceived;
        
        public bool IsWeaponReceived
        {
            get => _isWeaponReceived;
            set
            {
                _isWeaponReceived = value;
                
                if (_isWeaponReceived)
                {
                    _animator.SetBool("IsWeaponReceived", true);
                }
            }
        } //Problem here, we need to save this value between scenes

        public void TakeDamage(int amount)
        {
            Health -= amount;

            if (Health > 0) 
                return;
            
            _movingSpeed = 0;
            _isWeaponReceived = false;
            OnPlayerDeath?.Invoke();
        }
        
        public void SetActionAllowed(bool isAllowed)
        {
            _isActionAllowed = isAllowed;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f; 
            _rigidbody2D.rotation = _playerTransform.eulerAngles.z;

            _rigidbody2D.constraints = !isAllowed ? RigidbodyConstraints2D.FreezeAll :
                RigidbodyConstraints2D.None;
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _mainCamera = Camera.main;
            _playerTransform = transform;
            
            // IsWeaponReceived = true;
        }

        private void FixedUpdate()
        {
            if (!_isActionAllowed)
                return;
            
            Move();
            RotateTowardsMouse();
        }

        private void Update()
        {
            if (!_isActionAllowed)
                return;
            
            Attack();
        }

        private void Move()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var movement = new Vector2(horizontalInput, verticalInput);

            _rigidbody2D.velocity = movement * _movingSpeed;
        }

        private void RotateTowardsMouse()
        {
            var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var position = _playerTransform.position;
            var angle = Mathf.Atan2(mousePos.y - position.y, mousePos.x - position.x) * Mathf.Rad2Deg;

            _rigidbody2D.rotation = Mathf.LerpAngle(_rigidbody2D.rotation, angle, _rotationSpeed * Time.deltaTime);
        }

        private void Attack()
        {
            if (!IsWeaponReceived || !Input.GetMouseButtonDown(0)) 
                return;
            
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
                
            StartCoroutine(SwordAttack());
        }
        
        private IEnumerator SwordAttack()
        {
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(_beforeAttackDuration);
            _swordCollider.enabled = true;
            yield return new WaitForSeconds(_attackDuration);
            _swordCollider.enabled = false;
        }
    }
}

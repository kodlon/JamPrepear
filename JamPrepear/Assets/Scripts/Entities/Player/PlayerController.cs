using System;
using System.Collections;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _movingSpeed = 5.0f;
        [SerializeField] private float _rotationSpeed = 200.0f;
        [SerializeField] private float _cameraFollowSpeed = 0.125f;
        
        [SerializeField] private Collider2D _swordCollider;
        [SerializeField] private float _attackDuration = 0.1f;
        
        [SerializeField] private int _healthPoints = 3;
        
        public int Health
        {
            get => _healthPoints;
            set => _healthPoints = Mathf.Clamp(value, 0, int.MaxValue);
        }
        
        public event Action OnPlayerDeath;

        private Rigidbody2D _rigidbody2D;
        private Camera _mainCamera;
        private Transform _playerTransform;
        private Transform _mainCameraTransform;

        public void TakeDamage(int amount)
        {
            _healthPoints -= amount;

            if (_healthPoints <= 0)
                OnPlayerDeath?.Invoke();
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _mainCamera = Camera.main;
            _playerTransform = transform;
            _mainCameraTransform = _mainCamera.transform;
        }

        private void FixedUpdate()
        {
            Move();
            RotateTowardsMouse();
            FollowCamera();
        }

        private void Update()
        {
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
            if (Input.GetMouseButtonDown(0))
                StartCoroutine(SwordAttack());
        }

        private void FollowCamera()
        {
            var desiredPosition = _playerTransform.position;
            var position = _mainCameraTransform.position;
            var smoothedPosition = Vector3.Lerp(position, desiredPosition, _cameraFollowSpeed);
            position = new Vector3(smoothedPosition.x, smoothedPosition.y, position.z);
            _mainCamera.transform.position = position;
        }
        
        private IEnumerator SwordAttack()
        {
            _swordCollider.enabled = true;
            yield return new WaitForSeconds(_attackDuration);
            _swordCollider.enabled = false;
        }
    }
}

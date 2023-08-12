using UnityEngine;

namespace Entities.Enemies
{
    public class RotatingEnemy : EnemyBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed = 5.0f;

        private void FixedUpdate()
        {
            if (_target != null)
                RotateTowardsPlayer();
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
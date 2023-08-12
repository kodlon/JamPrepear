using UnityEngine;

namespace CameraUtils
{
    public class CameraFollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _cameraFollowSpeed = 0.125f;

        private Transform _cameraTransform;
        private Vector3 _shakeOffset = Vector3.zero;
        
        public void SetShakeOffset(Vector3 offset)
        {
            _shakeOffset = offset;
        }

        private void Awake()
        {
            _cameraTransform = transform;
        }

        private void FixedUpdate()
        {
            FollowCamera();
        }

        private void FollowCamera()
        {
            var desiredPosition = _target.position + _shakeOffset;
            var position = _cameraTransform.position;
            var smoothedPosition = Vector3.Lerp(position, desiredPosition, _cameraFollowSpeed);
            position = new Vector3(smoothedPosition.x, smoothedPosition.y, position.z);
            _cameraTransform.position = position;
        }
    }
}
using System;
using UnityEngine;

namespace CameraUtils
{
    public class CameraFollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _cameraFollowSpeed = 0.125f;
        
        private Transform _cameraTransform;

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
            var desiredPosition = _target.position;
            var position = _cameraTransform.position;
            var smoothedPosition = Vector3.Lerp(position, desiredPosition, _cameraFollowSpeed);
            position = new Vector3(smoothedPosition.x, smoothedPosition.y, position.z);
            _cameraTransform.transform.position = position;
        }
    }
}
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CameraUtils
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraFollowTarget _cameraFollowTarget;
        [SerializeField] private CameraShake _cameraShake;

        private Vector3 _shakeOffset = Vector3.zero;
        
        private void FixedUpdate()
        {
            if (_cameraShake.IsShaking)
                _shakeOffset = Random.insideUnitSphere * _cameraShake.ShakeAmount;
            else
                _shakeOffset = Vector3.zero;
            

            _cameraFollowTarget.SetShakeOffset(_shakeOffset);
        }
    }
}
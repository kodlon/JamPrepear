using UnityEngine;

namespace CameraUtils
{
    public class CameraShake : MonoBehaviour
    {
        public bool IsShaking => _currentShakeDuration > 0;
        public float ShakeAmount { get; private set; }

        private float _currentShakeDuration;

        private void Update()
        {
            if (_currentShakeDuration > 0)
                _currentShakeDuration -= Time.deltaTime;
            else
                _currentShakeDuration = 0f;
        }

        public void ShakeCamera(float customDuration, float customAmount)
        {
            _currentShakeDuration = customDuration;
            ShakeAmount = customAmount;
        }
    }
}
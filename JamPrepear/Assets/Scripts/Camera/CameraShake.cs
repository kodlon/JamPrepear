using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeAmount = 0.1f;

    private Vector3 _originalLocalPosition;
    private float _currentShakeDuration;

    private void Start()
    {
        _originalLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (_currentShakeDuration > 0)
        {
            transform.localPosition = _originalLocalPosition + Random.insideUnitSphere * _shakeAmount;
            _currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            _currentShakeDuration = 0f;
            transform.localPosition = _originalLocalPosition;
        }
    }

    public void ShakeCamera(float customDuration, float customAmount)
    {
        _currentShakeDuration = customDuration;
        _shakeAmount = customAmount;
    }
}
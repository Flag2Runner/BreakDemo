using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeManginitue = 0.2f;

    private Vector3 _origLocalPos;
    bool _bIsShaking = false;

    public void StartShake()
    {
        _bIsShaking = true;
        Invoke("StopShake", shakeDuration);
    }

    private void StopShake()
    {
        _bIsShaking = false;
        transform.localPosition = _origLocalPos;
    }

    private void LateUpdate()
    {
        if (_bIsShaking)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeManginitue;
            transform.localPosition += shakeOffset; 
        }
    }
}


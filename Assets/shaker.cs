using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaker : MonoBehaviour
{
    // Camera Information
    public Transform cameraTransform;
    private Vector3 orignalCameraPos;

    // Shake Parameters
    float shakeDuration;   float shakeAmount;

    private bool canShake = false;
    private float _shakeTimer;



    // Start is called before the first frame update
    void Start()
    {
        orignalCameraPos = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (canShake)
        {
            StartCameraShakeEffect();
        }
    }

    public void ShakeCamera(float dur, float mag)
    {
        canShake = true;
        shakeDuration = dur;
        shakeAmount = mag;
        _shakeTimer = shakeDuration;
    }

    public void StartCameraShakeEffect()
    {
        if (_shakeTimer > 0)
        {
            cameraTransform.localPosition = orignalCameraPos + Random.insideUnitSphere * shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            cameraTransform.localPosition = orignalCameraPos;
            canShake = false;
        }
    }
}

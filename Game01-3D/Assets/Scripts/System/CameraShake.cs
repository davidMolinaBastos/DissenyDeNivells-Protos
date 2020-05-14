using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;

    public float shakeDuration = 0f;

    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    private void Awake()
    {
        if (camTransform == null)
            camTransform = transform;
    }
    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }
    private void Update()
    {
        if(shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            enabled = false;
        }
    }
}

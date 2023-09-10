using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNCameraController : MonoBehaviour
{
    private Transform shakeTransform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 1.0f;

    private Vector3 initialPosition;

    void Awake() {
        shakeTransform = transform; // ���� ��ũ��Ʈ�� ����� GameObject�� Transform�� ������
    }

    void OnEnable() {
        initialPosition = shakeTransform.localPosition;
    }

    void Update() {
        if (shakeDuration > 0) {
            shakeTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else {
            shakeDuration = 0f;
            shakeTransform.localPosition = initialPosition;
        }
    }

    // ��鸲�� �����ϴ� �Լ�
    public void StartShake(float duration, float magnitude) {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

}

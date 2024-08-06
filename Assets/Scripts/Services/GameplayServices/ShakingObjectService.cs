using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingObjectService
{
    private Vector3 _originalPosition;
    private Transform _targetTransform;
    private float _maxShakeAmount;
    private float _frequency;

    public ShakingObjectService(Transform targetTransform, float maxShakeAmount, float frequency)
    {
        _targetTransform = targetTransform;
        _originalPosition = targetTransform.localPosition;
        _maxShakeAmount = maxShakeAmount;
        _frequency = frequency;
    }

    public void Shake(float elapsed)
    {
        float shakeAmount = Mathf.Lerp(0, _maxShakeAmount, elapsed);

        // Используем синусоидальную функцию для управления частотой колебаний
        float offsetX = Mathf.Sin(Time.time * _frequency) * shakeAmount;
        float offsetY = Mathf.Cos(Time.time * _frequency) * shakeAmount;

        // Применяем смещение к оригинальной позиции
        Vector3 randomOffset = new Vector3(offsetX, offsetY, 0);
        _targetTransform.localPosition = _originalPosition + randomOffset;
    }

    public void ResetPosition()
    {
        _targetTransform.localPosition = _originalPosition;
    }
}

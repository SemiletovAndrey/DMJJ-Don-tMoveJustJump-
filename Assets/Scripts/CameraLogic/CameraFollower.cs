using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _following;

    public float RotationAngleX;
    public float Distance;
    public float offsetY;
    public float rotationSpeed = 100f;

    private float _currentRotationAngleY;
    private float _initialRotationAngleY = 0f;
    private float returnDuration = 0.4f;
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }


    private void LateUpdate()
    {
        if (_following == null)
            return;

        if (_inputService.IsTurnLeftCameraButton())
        {
            if (_currentRotationAngleY < -360)
            {
                _currentRotationAngleY = 0;
            }
            _currentRotationAngleY -= rotationSpeed * Time.deltaTime;
        }
        if (_inputService.IsTurnRightCameraButton())
        {
            if (_currentRotationAngleY > 360)
            {
                _currentRotationAngleY = 0;
            }
            _currentRotationAngleY += rotationSpeed * Time.deltaTime;
        }
        if (_inputService.IsResetCameraButton())
        {
            StartCoroutine(ReturnToInitialPosition());
        }


        Quaternion rotation = Quaternion.Euler(RotationAngleX, _currentRotationAngleY, 0f);
        Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Follow(GameObject following) =>
        _following = following.transform;


    private IEnumerator ReturnToInitialPosition()
    {
        float elapsedTime = 0f;
        float startRotationAngleY = _currentRotationAngleY;

        while (elapsedTime < returnDuration)
        {
            float t = elapsedTime / returnDuration;
            t = t * t * (3f - 2f * t);
            _currentRotationAngleY = Mathf.Lerp(startRotationAngleY, _initialRotationAngleY, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentRotationAngleY = _initialRotationAngleY;
    }


    private Vector3 FollowingPointPosition()
    {
        Vector3 followingPosition = _following.position;
        followingPosition.y += offsetY;
        return followingPosition;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _following;

    public float RotationAngleX;
    public float Distance;
    public float offsetY;
    public float rotationSpeed = 100f;

    private float _currentRotationAngleY;
    private float _initialRotationAngleY = 0f;
    private float returnDuration = 0.2f;

    private void LateUpdate()
    {
        if (_following == null)
            return;

        if (Input.GetKey(KeyCode.Q))
        {
            if (_currentRotationAngleY < -360)
            {
                _currentRotationAngleY = 0;
            }
            _currentRotationAngleY -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (_currentRotationAngleY > 360)
            {
                _currentRotationAngleY = 0;
            }
            _currentRotationAngleY += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.R))
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

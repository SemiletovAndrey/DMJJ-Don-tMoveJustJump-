using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorTrigger : MonoBehaviour
{
    public Transform doorModel;
    public Transform doorController;
    public float duration = 3.0f;

    [SerializeField] private bool IsEnter;
    
    private ScaleYService _scaleYService;

    private void OnEnable()
    {
        _scaleYService = new ScaleYService(doorModel);
    }

    private void OnDisable()
    {
        StopCoroutine(OpenDoorCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsEnter)
        {
            OpenDoor();
        }
        else if (!IsEnter)
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        Debug.Log("OpenDoor");
        StartCoroutine(OpenDoorCoroutine());
    }

    public void CloseDoor()
    {
        Debug.Log("CloseDoor");
        StartCoroutine(CloseDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            this._scaleYService.ScaleDown(duration,elapsed);
            yield return null;
        }
    }
    private IEnumerator CloseDoorCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            this._scaleYService.ScaleUp(duration,elapsed);
            yield return null;
        }
        doorController.gameObject.SetActive(false);
    }

}

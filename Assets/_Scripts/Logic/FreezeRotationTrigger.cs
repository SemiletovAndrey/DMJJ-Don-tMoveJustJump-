using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotationTrigger : MonoBehaviour
{
    [SerializeField] private bool IsFreezing;

    private bool _isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered)
        {
            if (other.TryGetComponent<HeroMove>(out HeroMove heroMove))
            {
                if(!IsFreezing)
                {
                    FreezeRotation(other);
                }
                else if (IsFreezing)
                {
                    UnFreezeRotation(other);
                }
            }
        }
        
    }

    private void FreezeRotation(Collider other)
    {
        Rigidbody playerRb = other.GetComponent<Rigidbody>();
        playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        _isTriggered = true;
        Debug.Log("Freeze");
    }
    
    private void UnFreezeRotation(Collider other)
    {
        Rigidbody playerRb = other.GetComponent<Rigidbody>();
        playerRb.constraints = RigidbodyConstraints.None;
    }
}

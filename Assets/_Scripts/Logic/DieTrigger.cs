using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HeroDeath>())
        {
            EventBus.OnHeroDeath?.Invoke();
        }
    }
}

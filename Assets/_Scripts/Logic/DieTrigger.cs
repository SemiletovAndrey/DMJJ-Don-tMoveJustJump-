using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DieTrigger : MonoBehaviour
{
    [Inject] private IEventBus _eventBus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HeroDeath>())
        {
            _eventBus.Publish("OnHeroDeath");
        }
    }
}

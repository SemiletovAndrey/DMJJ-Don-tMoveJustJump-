using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeath : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private float delayTimeDeath = 3f;

    public Action OnDeath;

    private Coroutine _coroutineDeath;
    private bool _isLieDown = false;


    private void Start()
    {
        OnDeath += OnDeathHandler;
    }

    private void OnDestroy()
    {
        OnDeath -= OnDeathHandler;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (!_isLieDown)
        {
            Vector3 playerUp = transform.up;
            Vector3 worldUp = Vector3.up;
            float angle = Vector3.Angle(playerUp, worldUp);
            if (angle > 60)
            {
                _isLieDown = true;
                Debug.Log("Character lie down");
                _coroutineDeath = StartCoroutine(DeathCoroutine());
            }
        }
    }

    private IEnumerator DeathCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < delayTimeDeath)
        {
            if (CheckUpCharacter())
            {
                _coroutineDeath = null;
                Debug.Log("Up ");
                yield break;
            }
            elapsed += Time.deltaTime;
            ChangeColor(elapsed);
            yield return null;
        }
        OnDeath?.Invoke();
    }

    private bool CheckUpCharacter()
    {
        Vector3 playerUp = transform.up;
        Vector3 worldUp = Vector3.up;
        float angle = Vector3.Angle(playerUp, worldUp);
        if (angle <= 60)
        {
            _isLieDown = false;
            return true;
        }
        return false;
    }

    private void ChangeColor(float elapsed)
    {
        //TO DO
    }

    private void OnDeathHandler()
    {
        Debug.Log("Die");
        StopCoroutine(_coroutineDeath);
    }
}

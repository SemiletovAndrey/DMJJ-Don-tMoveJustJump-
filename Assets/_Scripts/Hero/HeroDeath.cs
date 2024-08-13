using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class HeroDeath : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private float delayTimeDeath = 3f;

    public Action OnDeath;

    private Coroutine _coroutineDeath;
    private bool _isLieDown = false;
    private ChangeColorService _changeColorService;
    [SerializeField] private ShakingObjectService _shakingObjectService;
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private Transform transformShaking;

    private CharacterSettings _characterSettings;

    [Inject]
    public void Construct(CharacterSettings characterSettings)
    {
        _characterSettings = characterSettings;
    }

    private void Start()
    {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
        OnDeath += OnDeathHandler;
        _changeColorService = new ChangeColorService(_renderers, _characterSettings.HeroDeathColor);
        _shakingObjectService = new ShakingObjectService(transformShaking, _characterSettings.MaxShakeAmount, _characterSettings.FrequencyDeath);
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
                _changeColorService.ResetColor();
                _shakingObjectService.ResetPosition();
                Debug.Log("Up ");
                yield break;
            }
            elapsed += Time.deltaTime;
            if (elapsed <= delayTimeDeath / 2)
            {
                _changeColorService.ChangeColor(elapsed / delayTimeDeath);
            }
            else
            {
                Debug.Log("Shake");
                _shakingObjectService.Shake(elapsed / delayTimeDeath);
            }
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

    private void OnDeathHandler()
    {
        Debug.Log("Die");
        _shakingObjectService.ResetPosition();
        StopCoroutine(_coroutineDeath);
    }
}

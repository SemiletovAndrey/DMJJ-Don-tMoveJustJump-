using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class HeroDeath : MonoBehaviour
{
    [SerializeField] private ShakingObjectService _shakingObjectService;
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private Transform transformShaking;


    public Action OnDeath;

    private Coroutine _coroutineDeath;
    private bool _isLieDown = false;
    private ChangeColorService _changeColorService;
    private CharacterSettings _characterSettings;
    private float _delayTimeDeath;
    private IEventBus _eventBus;

    [Inject]
    public void Construct(CharacterSettings characterSettings, IEventBus eventBus)
    {
        _characterSettings = characterSettings;
        _eventBus = eventBus;
    }

    private void Start()
    {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
        OnDeath += OnDeathHandler;
        _eventBus.Subscribe("OnHeroDeath", Die);
        _changeColorService = new ChangeColorService(_renderers, _characterSettings.HeroDeathColor);
        _shakingObjectService = new ShakingObjectService(transformShaking, _characterSettings.MaxShakeAmount, _characterSettings.FrequencyDeath);
        _delayTimeDeath = _characterSettings.DelayTimeDeath;
    }

    private void OnEnable()
    {
        _isLieDown = false;
    }

    private void OnDestroy()
    {
        OnDeath -= OnDeathHandler;
        _eventBus.Unsubscribe("OnHeroDeath", Die);
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
        while (elapsed < _delayTimeDeath)
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
            if (elapsed <= _delayTimeDeath / 2)
            {
                _changeColorService.ChangeColor(elapsed / _delayTimeDeath);
            }
            else
            {
                Debug.Log("Shake");
                _shakingObjectService.Shake(elapsed / _delayTimeDeath);
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
        _eventBus.Publish("OnHeroDeath");
    }

    public void ResetColor()
    {
        _changeColorService.ResetColor();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        Instantiate(_characterSettings.DieParticleEffects, transform.position, Quaternion.identity);
    }
}

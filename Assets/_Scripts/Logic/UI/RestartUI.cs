using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private Canvas DieCanvas;

    [Inject] private DiContainer _container;
    [Inject] private IPersistantProgressService _persistantProgress;
    private GameObject _player;
    private RestartService _restartService;

    private void Start()
    {
        DieCanvas.gameObject.SetActive(false);
        EventBus.OnHeroDeath += OnHeroDeath;
    }

    private void OnDestroy()
    {
        EventBus.OnHeroDeath -= OnHeroDeath;
    }

    public void Restart()
    {
        if (_player == null)
        {
            _player = _container.ResolveId<GameObject>("Player");
        }
        _restartService = new RestartService(_player,_persistantProgress);
        _restartService.Restart();
        SetActiveOffRestartCanvas();
    }

    private void SetActiveOnRestartCanvas()
    {
        DieCanvas.gameObject.SetActive(true);
    }

    private void SetActiveOffRestartCanvas()
    {
        DieCanvas.gameObject.SetActive(false);
    }

    private void OnHeroDeath()
    {
        SetActiveOnRestartCanvas();
    }
}

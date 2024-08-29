using System.Collections;
using UnityEngine;
using Zenject;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private Canvas DieCanvas;

    [Inject] private DiContainer _container;
    [Inject] private IPersistantProgressService _persistantProgress;
    [Inject] private IGameStateMachine _gameStateMachine;
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
        _restartService = new RestartService(_player,_persistantProgress, _gameStateMachine);
        _restartService.Restart();
        SetActiveOffRestartCanvas();
    }

    public void HardRestart()
    {
        if (_player == null)
        {
            _player = _container.ResolveId<GameObject>("Player");
        }
        _restartService = new RestartService(_player, _persistantProgress, _gameStateMachine);
        _restartService.HardRestart();
        SetActiveOffRestartCanvas();
    }

    private void SetActiveOnRestartCanvas()
    {
        StartCoroutine(SetActiveRestartCoroutine());
    }

    private void SetActiveOffRestartCanvas()
    {
        DieCanvas.gameObject.SetActive(false);
    }

    private void OnHeroDeath()
    {
        SetActiveOnRestartCanvas();
    }

    private IEnumerator SetActiveRestartCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        DieCanvas.gameObject.SetActive(true);
    }

}

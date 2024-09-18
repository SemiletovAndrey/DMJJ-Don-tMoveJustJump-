using System.Collections;
using UnityEngine;
using Zenject;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private Canvas DieCanvas;

    [Inject] private DiContainer _container;
    [Inject] private IPersistantProgressService _persistantProgress;
    [Inject] private IGameStateMachine _gameStateMachine;
    [Inject(Id = "RestartContainer")]private RectTransform _restartRectContainer;
    private GameObject _player;
    private RestartService _restartService;
    private UIWindowAnimator _windowAnimator;


    private void Start()
    {
        _windowAnimator = new UIWindowAnimator(_restartRectContainer);  
        DieCanvas.gameObject.SetActive(false);
        EventBus.OnHeroDeath += OnHeroDeath;
        EventBus.OnRestart += Restart;
        EventBus.OnHardRestart += HardRestart;
    }

    private void OnDestroy()
    {
        EventBus.OnHeroDeath -= OnHeroDeath;
        EventBus.OnRestart -= Restart;
        EventBus.OnHardRestart -= HardRestart;
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
        _gameStateMachine.Enter<GameLoopState>();

    }

    public void HardRestart()
    {
        if (_player == null)
        {
            _player = _container.ResolveId<GameObject>("Player");
        }
        _restartService = new RestartService(_player, _persistantProgress, _gameStateMachine);
        _restartService.HardRestart();
        _gameStateMachine.Enter<GameLoopState>();
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
        _windowAnimator.AnimateExpandWindow(Vector3.one);
    }

}

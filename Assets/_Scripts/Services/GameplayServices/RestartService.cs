using UnityEngine;

public class RestartService
{
    private GameObject _player;
    private IPersistantProgressService _progressService;
    private IGameStateMachine _gameStateMachine;

    public RestartService(GameObject player, IPersistantProgressService progressService, IGameStateMachine gameStateMachine)
    {
        _player = player;
        _progressService = progressService;
        this._gameStateMachine = gameStateMachine;
    }

    public void Restart()
    {
        _player.SetActive(true);
        HeroMove heroMove = _player.GetComponent<HeroMove>();
        heroMove.LoadProgress(_progressService.Progress);
        heroMove.RestartRotation();
        _player.GetComponent<HeroDeath>().ResetColor();
    }

    public void HardRestart()
    {
        _player.SetActive(false);
        _gameStateMachine.Enter<HardRestartStates>();
    }
}

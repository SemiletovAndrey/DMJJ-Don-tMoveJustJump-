using UnityEngine.SceneManagement;

public class LoadProgressState : IState
{
    private const string LevelInitNameConst = "TrainRoom";
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IPersistantProgressService _progressService;
    private readonly ISaveProgressService _saveProgressService;

    public LoadProgressState(IGameStateMachine gameStateMachine, IPersistantProgressService progressService, ISaveProgressService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveProgressService = saveLoadService;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
        PlayerProgress progress = _saveProgressService.LoadProgress();
        if(progress != null)
        {
            _progressService.Progress = progress;
            //_progressService.Progress.WorldData.PositionOnLevel.Level = progress.WorldData.PositionOnLevel.Level;
            //_progressService.Progress.WorldData.PositionOnLevel.Position = progress.WorldData.PositionOnLevel.Position;
            //_progressService.Progress.WorldData.PositionOnLevel.CurrentCheckpointIndex = progress.WorldData.PositionOnLevel.CurrentCheckpointIndex;
        }
        else
        {
            NewProgress();
        }
    }

    private PlayerProgress NewProgress()
    {
        _progressService.Progress.WorldData.PositionOnLevel.Level = LevelInitNameConst;
        return _progressService.Progress;
    }
}

using UnityEngine;
using Zenject;

public class SaveTrigger : MonoBehaviour, ISavedProgress
{
    private ISaveProgressService _saveProgressService;
    private IStaticDataService _staticDataService;
    private IPersistantProgressService _persistantProgressService;

    public int _currentPositionCheckpointIndex;

    [Inject]
    public void Construct(ISaveProgressService saveProgressService, IStaticDataService staticDataService, IPersistantProgressService persistantProgressService)
    {
        _saveProgressService = saveProgressService;
        _staticDataService = staticDataService;
        _persistantProgressService = persistantProgressService;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (SceneStaticService.CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
        {
            _currentPositionCheckpointIndex = progress.WorldData.PositionOnLevel.CurrentCheckpointIndex;
        }
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel.CurrentCheckpointIndex = _currentPositionCheckpointIndex;
    }

    [ContextMenu("Check progress")]
    public void CheckProgress()
    {
        _currentPositionCheckpointIndex = _persistantProgressService.Progress.WorldData.PositionOnLevel.CurrentCheckpointIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentPositionCheckpointIndex++;
        LevelStaticData levelData = _staticDataService.GetLevelStaticData();
        if (_currentPositionCheckpointIndex < levelData.Checkpoints.Count - 1)
        {
            transform.position = levelData.Checkpoints[_currentPositionCheckpointIndex];
            _saveProgressService.SaveProgress();
        }
        else
        {
            _saveProgressService.SaveProgress();
            gameObject.SetActive(false);
        }
    }
}

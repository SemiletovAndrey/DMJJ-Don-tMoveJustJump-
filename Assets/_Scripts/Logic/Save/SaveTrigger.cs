using UnityEngine;
using Zenject;

public class SaveTrigger : MonoBehaviour
{
    private ISaveProgressService _saveProgressService;
    private IStaticDataService _staticDataService;

    [Inject]
    public void Construct(ISaveProgressService saveProgressService, IStaticDataService staticDataService)
    {
        _saveProgressService = saveProgressService;
        _staticDataService = staticDataService;
    }

    private void OnTriggerEnter(Collider other)
    {
        _saveProgressService.SaveProgress();
        LevelStaticData levelData = _staticDataService.GetLevelStaticData();
        if (levelData.CurrentCheckpointIndex < levelData.Checkpoints.Count - 1)
        {
            levelData.CurrentCheckpointIndex++;
            transform.position = levelData.Checkpoints[levelData.CurrentCheckpointIndex];
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

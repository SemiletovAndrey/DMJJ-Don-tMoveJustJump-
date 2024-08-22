using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SaveTrigger : MonoBehaviour
{
    private ISaveProgressService _saveProgressService;
    private IPersistantProgressService _persistantProgressService;

    [Inject]
    public void Construct(ISaveProgressService saveProgressService, IPersistantProgressService persistantProgressService)
    {
        _saveProgressService = saveProgressService;
        _persistantProgressService = persistantProgressService;
    }

    private void OnTriggerEnter(Collider other)
    {
        _saveProgressService.SaveProgress();
        Debug.Log($"On Save trigger{_persistantProgressService.Progress.WorldData.PositionOnLevel.Position.Z}");
        gameObject.SetActive(false);
    }
}

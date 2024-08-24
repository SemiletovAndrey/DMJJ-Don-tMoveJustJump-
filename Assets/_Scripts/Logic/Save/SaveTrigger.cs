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
        gameObject.SetActive(false);
    }
}

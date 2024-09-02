using UnityEngine;

public class LevelTransfer : MonoBehaviour
{
    public string TransferTo;

    private IGameStateMachine _stateMachine;
    private IPersistantProgressService _progressService;
    private bool _triggered;

    public void Construct(IGameStateMachine gameStateMachine, IPersistantProgressService persistantProgressService)
    {
        _stateMachine = gameStateMachine;
        _progressService = persistantProgressService;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
            return;

        if (other.TryGetComponent<HeroMove>(out HeroMove heroMove))
        {
            heroMove.gameObject.SetActive(false);
            _stateMachine.Enter<NextLevelTransferState, string>(TransferTo);
            _progressService.Progress.WorldData.PositionOnLevel.CurrentCheckpointIndex = 0;
            _triggered = true;
        }
    }

}

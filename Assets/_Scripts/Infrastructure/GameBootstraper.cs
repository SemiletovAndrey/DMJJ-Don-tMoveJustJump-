using UnityEngine;
using Zenject;

public class GameBootstraper : MonoBehaviour
{
    private IGameStateMachine _gameStateMachine;
    

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void Start()
    {
        _gameStateMachine.Enter<BootstrapState>();
        //Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }
}

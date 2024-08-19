using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProgressState : IState
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IPersistantProgressService _progressService;
    private readonly ISaveProgressService _saveLoadService;

    public LoadProgressState(IGameStateMachine gameStateMachine, IPersistantProgressService progressService, ISaveProgressService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
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
        //_progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
    }

    //private PlayerProgress NewProgress()
    //{
    //    var progress = new PlayerProgress("LevelTest");

    //    progress.HeroState.MaxHP = 50;
    //    progress.HeroState.ResetHP();
    //    progress.HeroStats.Damage = 1;
    //    progress.HeroStats.DamageRadius = 0.5f;

    //    return progress;
    //}
}

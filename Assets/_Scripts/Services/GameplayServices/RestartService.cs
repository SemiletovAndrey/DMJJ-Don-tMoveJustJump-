using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RestartService
{
    private GameObject _player;
    private IPersistantProgressService _progressService;

    public RestartService(GameObject player, IPersistantProgressService progressService)
    {
        _player = player;
        _progressService = progressService;
    }

    public void Restart()
    {
        _player.SetActive(true);
        HeroMove heroMove = _player.GetComponent<HeroMove>();
        heroMove.LoadProgress(_progressService.Progress);
        heroMove.RestartRotation();
        _player.GetComponent<HeroDeath>().ResetColor();

    }
}

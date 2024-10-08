using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityFactory
{
    List<ISavedProgressReader> ProgressReader { get; }
    List<ISavedProgress> ProgressWriter { get; }

    public GameObject CreatePlayer(Vector3 position);
    public GameObject CreateHud();
    void CleanUp();
    GameObject CreateSaveTrigger(Vector3 position);
    GameObject CreateLevelTransfer(string transferTo,Vector3 position);
}

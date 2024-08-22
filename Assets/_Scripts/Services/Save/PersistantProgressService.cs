using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistantProgressService : IPersistantProgressService
{
    public PlayerProgress Progress { get; set; }
    public PersistantProgressService()
    {
        Progress = new PlayerProgress();
    }
}

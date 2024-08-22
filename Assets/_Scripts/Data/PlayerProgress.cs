using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerProgress
{
    public WorldData WorldData;
    public PlayerProgress()
    {
        WorldData = new WorldData();
    }
}

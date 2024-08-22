using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WorldData
{
    public PositionOnLevel PositionOnLevel;

    public WorldData()
    {
        PositionOnLevel = new PositionOnLevel();
    }
}

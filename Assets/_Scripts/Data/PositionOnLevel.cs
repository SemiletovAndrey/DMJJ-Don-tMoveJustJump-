using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PositionOnLevel
{
    public string Level;
    public Vector3Serial Position;
    public PositionOnLevel()
    {

    }

    public PositionOnLevel(string level, Vector3Serial position)
    {
        Level = level;
        Position = position;
    }
}

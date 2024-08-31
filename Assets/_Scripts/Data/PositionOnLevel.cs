using System;

[Serializable]
public class PositionOnLevel
{
    public string Level;
    public Vector3Serial Position;
    public int CurrentCheckpointIndex;

    public PositionOnLevel()
    {

    }

    public PositionOnLevel(string level, Vector3Serial position, int currentCheckpointIndex)
    {
        Level = level;
        Position = position;
        CurrentCheckpointIndex = currentCheckpointIndex;
    }
}

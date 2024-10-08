using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
public class LevelStaticData : ScriptableObject
{
    public string LevelKey;
    public List<Vector3> Checkpoints;
    public Vector3 InitialPlayerPoint;
    public LevelTransferStaticData LevelTransferStaticData;
}

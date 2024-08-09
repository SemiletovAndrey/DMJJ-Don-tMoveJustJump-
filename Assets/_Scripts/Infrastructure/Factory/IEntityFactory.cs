using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityFactory
{
    public GameObject CreatePlayer(Vector3 position);
    public GameObject CreateHud();
}

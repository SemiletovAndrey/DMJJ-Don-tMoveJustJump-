using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveProgressService
{
    public PlayerProgress LoadProgress();
    public void SaveProgress();
}

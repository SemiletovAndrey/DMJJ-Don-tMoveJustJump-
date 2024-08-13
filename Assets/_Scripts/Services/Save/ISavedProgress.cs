using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavedProgress: ISavedProgressReader
{
    public void UpdateProgress(PlayerProgress progress);
}

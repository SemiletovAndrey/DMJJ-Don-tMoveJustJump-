using UnityEngine;

public interface IAssetProvider
{
    public GameObject Instantiate(string path);
}

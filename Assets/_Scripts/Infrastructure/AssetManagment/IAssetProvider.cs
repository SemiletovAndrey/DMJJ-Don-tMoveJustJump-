using UnityEngine;

public interface IAssetProvider
{
    public GameObject Instantiate(string path);
    public GameObject Instantiate(string path, Vector3 at);
}

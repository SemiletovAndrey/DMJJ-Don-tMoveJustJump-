using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string path, Vector3 at) =>
        Resources.Load<GameObject>(path);

    public GameObject Instantiate(string path) => 
        Resources.Load<GameObject>(path);
}

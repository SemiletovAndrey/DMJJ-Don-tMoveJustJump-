using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntityFactory : IEntityFactory
{
    private readonly DiContainer _container;
    private IAssetProvider _assetProvider;


    public EntityFactory(DiContainer container, IAssetProvider assetProvider)
    {
        _container = container;
        _assetProvider = assetProvider;
    }

    public GameObject CreatePlayer(Vector3 position)
    {
        GameObject playerPrefab = _assetProvider.Instantiate(AssetAddress.HeroPath, position);
        return _container.InstantiatePrefab(playerPrefab);
    }

    public GameObject CreateHud()
    {
        IInputService inputService = _container.Resolve<IInputService>();
        GameObject hudPrefab;
        if (inputService is PCInputSystem)
        {
            hudPrefab = _assetProvider.Instantiate(AssetAddress.HudPCPath);
            Debug.Log("PC HUD will be instantiated");
        }
        else
        {
            hudPrefab = _assetProvider.Instantiate(AssetAddress.HudMobilePath);
            Debug.Log("Mobile HUD will be instantiated");
        }

        return _container.InstantiatePrefab(hudPrefab);
    }

}

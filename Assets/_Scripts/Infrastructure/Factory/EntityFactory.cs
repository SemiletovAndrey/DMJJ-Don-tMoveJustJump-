using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntityFactory : IEntityFactory
{
    private readonly DiContainer _container;
    private IAssetProvider _assetProvider;

    public List<ISavedProgressReader> ProgressReader { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriter { get; } = new List<ISavedProgress>();

    public EntityFactory(DiContainer container, IAssetProvider assetProvider)
    {
        _container = container;
        _assetProvider = assetProvider;
    }

    public GameObject CreatePlayer(Vector3 position)
    {
        GameObject player = InstantiateRegistered(AssetAddress.HeroPath, position, true);
        player.transform.position = position;
        _container.Bind<GameObject>().WithId("Player").FromInstance(player).AsSingle();
        return player;
    }

    public GameObject CreateHud()
    {
        IInputService inputService = _container.Resolve<IInputService>();
        GameObject hudPrefab;
        if (inputService is PCInputSystem)
        {
            hudPrefab = InstantiateRegistered(AssetAddress.HudPCPath);
        }
        else
        {
            hudPrefab = InstantiateRegistered(AssetAddress.HudMobilePath);
        }
        return _container.InstantiatePrefab(hudPrefab);
    }

    public GameObject CreateSaveTrigger(Vector3 position)
    {
        GameObject trigger = InstantiateRegistered(AssetAddress.SaveTriggerPath, position);
        trigger.transform.position = position;
        return _container.InstantiatePrefab(trigger);
    }

    public void CleanUp()
    {
        ProgressReader.Clear();
        ProgressWriter.Clear();
    }

    private GameObject InstantiateRegistered(string path, Vector3 position, bool pushInContainer = false)
    {
        GameObject gameObject = _assetProvider.Instantiate(path, position);
        if (pushInContainer)
        {
            gameObject = _container.InstantiatePrefab(gameObject);
        }
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }
    private GameObject InstantiateRegistered(string path)
    {
        GameObject gameObject = _assetProvider.Instantiate(path);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader reader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        {
            Register(reader);
            Debug.Log("Register");

        }
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
        {
            ProgressWriter.Add(progressWriter);
            Debug.Log("Register Writer");
        }
        ProgressReader.Add(progressReader);
    }
}

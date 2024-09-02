using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntityFactory : IEntityFactory
{
    private readonly DiContainer _container;
    private IAssetProvider _assetProvider;
    private IGameStateMachine _gameStateMachine;
    private IPersistantProgressService _persistantProgressService;

    public List<ISavedProgressReader> ProgressReader { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriter { get; } = new List<ISavedProgress>();

    public EntityFactory(DiContainer container, IAssetProvider assetProvider, IGameStateMachine gameStateMachine, IPersistantProgressService persistantProgressService)
    {
        _container = container;
        _assetProvider = assetProvider;
        _gameStateMachine = gameStateMachine;
        _persistantProgressService = persistantProgressService;
    }

    public GameObject CreatePlayer(Vector3 position)
    {
        GameObject player = InstantiateRegisteredWithContainer(AssetAddress.HeroPath, position);
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
        GameObject trigger = InstantiateRegisteredWithContainer(AssetAddress.SaveTriggerPath, position);
        trigger.transform.position = position;
        _container.Bind<SaveTrigger>().FromInstance(trigger.GetComponent<SaveTrigger>());
        return trigger;
    }
    
    public GameObject CreateLevelTransfer(string transferTo, Vector3 position)
    {
        GameObject transfer = InstantiateRegistered(AssetAddress.LevelTransferPath, position);
        LevelTransfer levelTransfer = transfer.GetComponent<LevelTransfer>();
        levelTransfer.Construct(_gameStateMachine, _persistantProgressService);
        levelTransfer.TransferTo = transferTo;
        transfer.transform.position = position;
        return transfer;
    }

    public void CleanUp()
    {
        ProgressReader.Clear();
        ProgressWriter.Clear();
    }

    private GameObject InstantiateRegisteredWithContainer(string path, Vector3 position)
    {
        GameObject gameObject = _assetProvider.Instantiate(path);
        gameObject = _container.InstantiatePrefab(gameObject);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }
    
    private GameObject InstantiateRegistered(string path, Vector3 position)
    {
        GameObject gameObject = _assetProvider.Instantiate(path);
        gameObject = GameObject.Instantiate(gameObject, position,Quaternion.identity);
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
        }
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
        {
            ProgressWriter.Add(progressWriter);
        }
        ProgressReader.Add(progressReader);
    }
}

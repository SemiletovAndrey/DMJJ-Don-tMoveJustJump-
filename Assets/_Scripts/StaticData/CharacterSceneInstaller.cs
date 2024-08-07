using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName ="CharacterSettings", menuName = "StaticData/Character Settings Data")]
public class CharacterSceneInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Color _targetCharacterColor;
    public override void InstallBindings()
    {
        Container.Bind<Color>().FromInstance(_targetCharacterColor).AsSingle().NonLazy();
    }
}

using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseUIInstaller : MonoInstaller
{
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Slider SensitivitySlider;
    public RectTransform PauseRectTransform;
    public RectTransform RestartRectTransform;

    public override void InstallBindings()
    {
        Container.Bind<Slider>().WithId("MusicSlider").FromInstance(MusicSlider);
        Container.Bind<Slider>().WithId("SoundSlider").FromInstance(SoundSlider);
        Container.Bind<Slider>().WithId("SensitivitySlider").FromInstance(SensitivitySlider);
        Container.Bind<RectTransform>().WithId("PauseContainer").FromInstance(PauseRectTransform);
        Container.Bind<RectTransform>().WithId("RestartContainer").FromInstance(RestartRectTransform);

        Container.Bind<HeroDeath>().FromComponentInHierarchy().AsSingle();
    }
}

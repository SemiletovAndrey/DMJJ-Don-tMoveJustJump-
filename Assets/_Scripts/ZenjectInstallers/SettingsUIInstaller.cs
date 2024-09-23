using TMPro;
using UnityEngine.UI;
using Zenject;

public class SettingsUIInstaller : MonoInstaller
{
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Slider SensitivitySlider;
    public TMP_Dropdown GraphicQuality;
    public Slider LockFPS;
    public TMP_Dropdown Language;

    public override void InstallBindings()
    {
        Container.Bind<Slider>().WithId("MusicSlider").FromInstance(MusicSlider);
        Container.Bind<Slider>().WithId("SoundSlider").FromInstance(SoundSlider);
        Container.Bind<Slider>().WithId("SensitivitySlider").FromInstance(SensitivitySlider);
        Container.Bind<TMP_Dropdown>().WithId("GraphicQuality").FromInstance(GraphicQuality);
        Container.Bind<Slider>().WithId("LockFPS").FromInstance(LockFPS);
        Container.Bind<TMP_Dropdown>().WithId("Language").FromInstance(Language);
    }
}

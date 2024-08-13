using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseUIInstaller : MonoInstaller
{
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Slider SensitivitySlider;

    public override void InstallBindings()
    {
        Container.Bind<Slider>().WithId("MusicSlider").FromInstance(MusicSlider);
        Container.Bind<Slider>().WithId("SoundSlider").FromInstance(SoundSlider);
        Container.Bind<Slider>().WithId("SensitivitySlider").FromInstance(SensitivitySlider);
    }
}

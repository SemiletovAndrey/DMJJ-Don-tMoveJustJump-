using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SnapSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textFps;
    [SerializeField] private float[] snapValues = { 30f, 45f, 60f };
    [SerializeField] private float snapRange = 5f;

    public float currentFPS {  get; private set; }

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        textFps.text = slider.value.ToString();
    }

    void OnSliderValueChanged(float value)
    {
        float closestValue = value;
        foreach (float snapValue in snapValues)
        {
            if (Mathf.Abs(value - snapValue) <= snapRange)
            {
                closestValue = snapValue;
                break;
            }
        }
        slider.value = closestValue;
        textFps.text = closestValue.ToString();
        currentFPS = closestValue;
    }
}

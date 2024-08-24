using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float fps;
    private float updateTimer = 0.2f;

    [SerializeField] private TextMeshProUGUI fpsTitle;

    private void Update()
    {
        UpdateFPSDisplay();
    }

    private void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0f)
        {
            fps = 1f / Time.unscaledDeltaTime;
            fpsTitle.text = $"FPS: {Mathf.Round(fps)}";
            updateTimer += 0.2f;
        }
    }
}

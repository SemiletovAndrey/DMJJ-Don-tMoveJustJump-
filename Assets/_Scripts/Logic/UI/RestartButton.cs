using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image backgroundImage;
    public float holdTime = 2f;

    private bool isHolding = false;
    private float holdTimer = 0f;

    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            backgroundImage.fillAmount = holdTimer / holdTime;
            if (holdTimer >= holdTime)
            {
                HardRestart();
                ResetHold();
            }
        }
    }

    public void OnButtonRelease()
    {
        isHolding = false;
        if (holdTimer < holdTime)
        {
            RegularRestart();
        }
        ResetHold();
    }

    private void ResetHold()
    {
        isHolding = false;
        holdTimer = 0f;
    }

    private void RegularRestart()
    {
        backgroundImage.fillAmount = 0;
        EventBus.OnRestart?.Invoke();
    }

    private void HardRestart()
    {
        backgroundImage.fillAmount = 0;
        EventBus.OnHardRestart?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonRelease();
    }
}

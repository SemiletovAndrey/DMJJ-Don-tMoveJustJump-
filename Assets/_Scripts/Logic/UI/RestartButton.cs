using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class RestartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Inject] private IEventBus _eventBus;

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
        _eventBus.Publish("OnRestart");
    }

    private void HardRestart()
    {
        backgroundImage.fillAmount = 0;
        _eventBus.Publish("OnHardRestart");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickZoneHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image targetImage;
    [SerializeField] private RectTransform targetTransform;

    public void OnPointerDown(PointerEventData eventData)
    {
        MoveButtonOnClick(eventData);
        targetImage.enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetImage.enabled = false;
    }

    private void MoveButtonOnClick(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                   targetTransform,
                   eventData.position,
                   eventData.pressEventCamera,
                   out Vector2 localPoint);

        targetImage.rectTransform.anchoredPosition = localPoint;
    }
}

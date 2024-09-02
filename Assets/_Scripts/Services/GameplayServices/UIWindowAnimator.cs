using DG.Tweening;
using System;
using UnityEngine;

public class UIWindowAnimator
{
    private RectTransform windowRect;
    private float startYPosition = 1000f;  
    private float endYPosition = -150f;   
    private float fallDuration = 0.35f;  
    private float upDuration = 0.2f;  
    private float bounceDuration = 0.05f;

    private float scaleDuration = 0.5f;

    public UIWindowAnimator(RectTransform pauseRect)
    {
        windowRect = pauseRect;
    }

    public void AnimateOnWindow(Action onComplete)
    {
        windowRect.anchoredPosition = new Vector2(windowRect.anchoredPosition.x, startYPosition);
        windowRect.DOAnchorPosY(endYPosition, fallDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                windowRect.DOAnchorPosY(endYPosition - 20f, bounceDuration)
                    .SetEase(Ease.OutQuad)
                    .SetLoops(2, LoopType.Yoyo);

                onComplete?.Invoke();
            });
    }

    public void AnimateOffWindow(Action onComplete)
    {
        windowRect.DOAnchorPosY(startYPosition, upDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    public void AnimateExpandWindow(Vector3 targetScale)
    {
        windowRect.localScale = Vector3.zero;
        windowRect.DOScale(targetScale, scaleDuration)
            .SetEase(Ease.OutBack);
    }
}


using System;
using DG.Tweening;
using UnityEngine;

public class DoTweenUtility
{
    public static Tween AnimateToTransform(Transform from, Transform to, float duration, Action onComplete = null)
    {
        return DOTween.Sequence()
            .Join(from.DOMove(to.position, duration))
            .Join(from.DORotate(to.rotation.eulerAngles, duration))
            .Join(from.DOScale(to.localScale, duration))
            .SetEase(Ease.InOutSine)
            .OnComplete(() => onComplete?.Invoke()); // <-- Run callback when complete
    }
}
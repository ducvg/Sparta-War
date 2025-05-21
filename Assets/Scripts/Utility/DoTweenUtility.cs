
using DG.Tweening;
using UnityEngine;

public class DoTweenUtility
{
    public static void AnimateToTransform(Transform from, Transform to, float duration)
    {
        DOTween.Sequence()
            .Join(from.DOMove(to.position, duration))
            .Join(from.DORotate(to.rotation.eulerAngles, duration))
            .Join(from.DOScale(to.localScale, duration))
            .SetEase(Ease.InOutSine);
    }
}
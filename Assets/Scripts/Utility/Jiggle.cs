using DG.Tweening;
using UnityEngine;

public class Jiggle : MonoBehaviour
{
    [SerializeField] private float jiggleCycleTime = 0.5f; 
    [SerializeField] private float pauseBetweenJiggles = 1f; 
    private Tween runningTween;

    void Start()
    {
        runningTween = DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, -4f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(new Vector3(0, 0, 4f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(Vector3.zero, jiggleCycleTime).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo);
    }
}

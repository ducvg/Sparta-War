using DG.Tweening;
using UnityEngine;

public class Grow : MonoBehaviour
{
    [SerializeField] private float growtarget = 1.3f; 
    [SerializeField] private float growCycleTime = 2f; 
    private Tween runningTween;

    void Start()
    {
        runningTween = DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(growtarget, growtarget, growtarget), growCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DOScale(new Vector3(growtarget, growtarget, growtarget), growCycleTime).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Restart);
    }
}

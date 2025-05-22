using DG.Tweening;
using UnityEngine;

public class Grow : MonoBehaviour
{
    [SerializeField] private float growtarget = 1.3f; 
    [SerializeField] private float growCycleTime = 2f; 
    private Tween runningTween;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;

        runningTween = DOTween.Sequence()
            .Append(transform.DOScale(initialScale * growtarget, growCycleTime).SetEase(Ease.InOutSine))  
            .Append(transform.DOScale(initialScale, growCycleTime).SetEase(Ease.InOutSine))               
            .SetLoops(-1, LoopType.Restart);                                                              
    }

}

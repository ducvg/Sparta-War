using DG.Tweening;
using UnityEngine;

public class TweenToHere : MonoBehaviour
{
    [SerializeField] private float cycleTime = 1f;
    [SerializeField] private Transform targetToTween;

    void Awake()
    {
        targetToTween = GameObject.FindGameObjectWithTag("Player").transform;
        TweenToTarget();
    }

    public void TweenToTarget()
    {
        if (targetToTween == null)
        {
            Debug.LogError("Target transform is not assigned.");
            return;
        }

        DoTweenUtility.AnimateToTransform(targetToTween, transform, cycleTime);
    }
}

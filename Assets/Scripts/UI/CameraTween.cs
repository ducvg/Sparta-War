using System;
using DG.Tweening;
using UnityEngine;

public class CameraTween : MonoBehaviour
{
    public float cycleTime = 2f;
    public Transform menuTransform;
    public Transform ingameTransform;

    public float jiggleCycleTime = 5f;

    private Tween runningTween;
    private Transform achorTransform;


    void Start()
    {
        achorTransform = transform;
        DoJiggle();
    }

    public void Menu()
    {
        DoTweenUtility.AnimateToTransform(transform, menuTransform, cycleTime);
        Invoke(nameof(DoJiggle), cycleTime + 0.01f);
    }

    // private void DoJiggle()
    // {
    //     runningTween = DOTween.Sequence()
    //         .Append(transform.DORotate(new Vector3(achorTransform.rotation.x, achorTransform.rotation.y, -4f), jiggleCycleTime).SetEase(Ease.InOutSine))
    //         .Append(transform.DORotate(new Vector3(achorTransform.rotation.x, achorTransform.rotation.y, 4f), jiggleCycleTime).SetEase(Ease.InOutSine))
    //         .Append(transform.DORotate(new Vector3(achorTransform.rotation.x, achorTransform.rotation.y, achorTransform.rotation.z), jiggleCycleTime).SetEase(Ease.InOutSine))
    //         .SetLoops(-1, LoopType.Restart);
    // }

    private void DoJiggle()
    {
        runningTween = DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(3.36f, 152.48f, -4f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(new Vector3(3.36f, 152.48f, 4f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(new Vector3(3.36f, 152.48f, 0), jiggleCycleTime).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Restart);
    }

    public void Ingame()
    {
        runningTween?.Kill();
        DoTweenUtility.AnimateToTransform(transform, ingameTransform, cycleTime);
    }
}

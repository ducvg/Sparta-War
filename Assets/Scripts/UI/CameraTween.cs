using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraTween : MonoBehaviour
{
    public float cycleTime = 2f;
    public Transform menuTransform;
    public Transform ingameTransform;

    public float jiggleCycleTime = 5f;

    private List<Tween> runningTween = new();
    private Transform achorTransform;


    void Start()
    {

        achorTransform = transform;
        DoJiggle();
    }

    public void Menu()
    {
        runningTween.Add(DoTweenUtility.AnimateToTransform(transform, menuTransform, cycleTime,
            () =>
            {
                DoJiggle();
            }));
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
        runningTween.Add (DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(3.36f, 152.48f, -4f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(new Vector3(3.36f, 152.48f, 4f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(new Vector3(3.36f, 152.48f, 0), jiggleCycleTime).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Restart));
    }

    public void Ingame()
    {
        foreach(var tween in runningTween)
        {
            tween.Kill();
        }
        runningTween.Clear();
        DoTweenUtility.AnimateToTransform(transform, ingameTransform, cycleTime);
    }
}

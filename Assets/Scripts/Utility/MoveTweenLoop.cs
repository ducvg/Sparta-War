using DG.Tweening;
using UnityEngine;

public class MoveTweenLoop : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;

    private void Start()
    {
        transform.DOMove(targetPosition, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    


}

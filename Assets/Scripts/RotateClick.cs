using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float cycleLength = 0.5f;
    [SerializeField] private bool isRotating = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRotating) return;
        isRotating = true;
        transform.DORotate(transform.eulerAngles + new Vector3(0, 90, 0), cycleLength, RotateMode.FastBeyond360).SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            isRotating = false;
        });
    }
}

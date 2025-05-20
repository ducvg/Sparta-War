using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RotateClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float cycleLength = 0.5f;
    [SerializeField] private bool isRotating = false;
    [SerializeField] private UnityEvent<Vector3, Vector3> onRotateComplete;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        if (isRotating) return;
        isRotating = true;
        transform.DORotate(transform.eulerAngles + new Vector3(0, 90, 0), cycleLength, RotateMode.FastBeyond360).SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            onRotateComplete.Invoke(transform.position, transform.forward);
            isRotating = false;
        });
    }
}

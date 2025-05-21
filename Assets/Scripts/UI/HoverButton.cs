using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float scaleCycleTime = 0.3f;
    [SerializeField] private float jiggleCycleTime = 0.3f;
    [SerializeField] private float pauseBetweenJiggles = 1f;
    
    private Sequence jiggleSequence;
    private Tween jiggleTween;
    public void OnPointerEnter(PointerEventData eventData)
    {
        jiggleSequence?.Kill();
        jiggleSequence = DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, -12.5f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(new Vector3(0, 0, 12.5f), jiggleCycleTime).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(Vector3.zero, jiggleCycleTime).SetEase(Ease.InOutSine))
            .AppendInterval(pauseBetweenJiggles)
            .SetLoops(-1, LoopType.Restart);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        jiggleTween?.Kill();
        jiggleSequence?.Kill();
        transform.DORotate(Vector3.zero, jiggleCycleTime)
            .SetEase(Ease.OutSine);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), scaleCycleTime)
            .SetEase(Ease.InOutSine);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), scaleCycleTime)
            .SetEase(Ease.InOutSine);
    }
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    public CameraTween cameraTween;
    public UnityEvent ingameEvent;
    [Header("Menu Components")]
    public List<TweenReact> menuComponents;
    public TweenReact settingPanel;
    public float cycleTime = 0.5f;

    public void Play()
    {
        ToggleMenu(false, () =>
        {
            GameManager.Instance.StartLevel(DataManager.gameData.playerData.currentLevelIndex);
            BGM.Instance.PlayIngameBGM();
            ingameEvent?.Invoke();
            cameraTween.Ingame();
            this.gameObject.SetActive(false);
        });
    }

    public void ToggleSetting(bool isShow)
    {
        ToggleMenu(!isShow, null);
        if(isShow) settingPanel.rectTransform.gameObject.SetActive(true);
        settingPanel.rectTransform.DOAnchorPos3D(isShow ? settingPanel.showPosition : settingPanel.hidePosition, cycleTime)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                if(!isShow) settingPanel.rectTransform.gameObject.SetActive(false);
            });
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleMenu(bool isShow, Action onComplete)
    {
        Sequence sequence = DOTween.Sequence();

        foreach (var component in menuComponents)
        {
            if(isShow) component.rectTransform.gameObject.SetActive(true);

            // Create tween for each component and join them all to play in parallel
            Tween moveTween = component.rectTransform
                .DOAnchorPos3D(isShow ? component.showPosition : component.hidePosition, cycleTime)
                .SetEase(Ease.InOutSine)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    if(!isShow) component.rectTransform.gameObject.SetActive(false);
                });

            sequence.Join(moveTween);
        }

        sequence.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }


}

using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenuController : MonoBehaviour
{
    public float cycleTime;
    [Header("Ingame UI")]
    public TweenReact attackBtn;
    public TweenReact pauseBtn, pausePanel, winPanel;
    public Image background;
    public MenuController menuController;
    public CameraTween cameraTween;

    public float fadeTargetAlpha = 0.25f;

    public void ReturnHomeMenu()
    {
        TogglePause(false);
        DoReturnHomeMenu();
    }

    private void DoReturnHomeMenu()
    {
        BGM.Instance.PlayMenuBGM();
        cameraTween.Menu();
        menuController.gameObject.SetActive(true);
        menuController.ToggleMenu(true, null);

        gameObject.SetActive(false);
    }

    public void ToggleWin(bool isShow)
    {
        GameState.IsGameWon = isShow;
        Time.timeScale = isShow ? 0 : 1;
        ToggleBtn(winPanel, isShow, true, Ease.OutBounce);
    }

    public void TogglePause(bool isShow)
    {
        if (GameState.IsGameWon) return;
        Time.timeScale = isShow ? 0 : 1;
        ToggleBtn(pausePanel, isShow, true, Ease.OutBounce);
    }

    public void ToggleAttackBtn(bool isShow)
    {
        ToggleBtn(attackBtn, isShow);
    }

    public void TogglePauseBtn(bool isShow)
    {
        ToggleBtn(pauseBtn, isShow);
    }

    public void ToggleBackground(bool isShow)
    {
        if (isShow) background.gameObject.SetActive(true);
        background.DOFade(isShow ? fadeTargetAlpha : 0, cycleTime)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                if (!isShow) background.gameObject.SetActive(false);
            });
    }

    public void ToggleBtn(TweenReact btn, bool isShow)
    {
        if (isShow) btn.rectTransform.gameObject.SetActive(true);
        btn.rectTransform.DOAnchorPos3D(isShow ? btn.showPosition : btn.hidePosition, cycleTime)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                if (!isShow) btn.rectTransform.gameObject.SetActive(false);
            });
    }

    public void ToggleBtn(TweenReact btn, bool isShow, bool hasBackground, Action onComplete = null)
    {
        if (isShow)
        {
            btn.rectTransform.gameObject.SetActive(true);
            if (hasBackground) background.gameObject.SetActive(true);
        }

        Sequence sequence = DOTween.Sequence().SetUpdate(true);

        if (hasBackground)
        {
            sequence.Append(background.DOFade(isShow ? fadeTargetAlpha : 0f, cycleTime)
                .SetUpdate(true));
        }

        sequence.Join(btn.rectTransform.DOAnchorPos3D(isShow ? btn.showPosition : btn.hidePosition, cycleTime)
            .SetUpdate(true));

        sequence.OnComplete(() =>
        {
            if (!isShow)
            {
                onComplete?.Invoke();
                btn.rectTransform.gameObject.SetActive(false);
                if (hasBackground) background.gameObject.SetActive(false);
            }

        });
    }
    
    public void ToggleBtn(TweenReact btn, bool isShow, bool hasBackground, Ease ease, Action onComplete = null)
    {
        if (isShow)
        {
            btn.rectTransform.gameObject.SetActive(true);
            if(hasBackground) background.gameObject.SetActive(true);
        }

        Sequence sequence = DOTween.Sequence().SetUpdate(true).SetEase(ease);

        if (hasBackground)
        {
            sequence.Append(background.DOFade(isShow ? fadeTargetAlpha : 0f, cycleTime)
                .SetUpdate(true));
        }

        sequence.Join(btn.rectTransform.DOAnchorPos3D(isShow ? btn.showPosition : btn.hidePosition, cycleTime)
            .SetUpdate(true));

        sequence.OnComplete(() =>
        {
            if (!isShow)
            {
                onComplete?.Invoke();
                btn.rectTransform.gameObject.SetActive(false);
                if (hasBackground) background.gameObject.SetActive(false);
            }
                
        });
    }

    
}

[Serializable]
public class TweenReact
{
    public RectTransform rectTransform;
    public Vector3 hidePosition, showPosition;
}

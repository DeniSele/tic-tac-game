using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenBase : MonoBehaviour
{
    [Header("Screen base settings")]
    [SerializeField] private ScreenType screenType;
    [SerializeField] private Transform root;
    [SerializeField] private CanvasGroup canvasGroup;

    private UiService uiService;


    public ScreenType ScreenType => screenType;


    public virtual void Initialize(UiService uiService)
    {
        this.uiService = uiService;
    }


    public virtual void Show(Action onShow = null)
    {
        root.gameObject.SetActive(true);

        DOVirtual.Float(0, 1, 1, (value) => canvasGroup.alpha = value)
            .SetEase(Ease.InSine)
            .OnComplete(() => 
                {
                    onShow?.Invoke();
                });
    }


    public virtual void Hide(bool immediately = false, Action onHide = null)
    {
        if (immediately)
        {
            canvasGroup.alpha = 0;
            root.gameObject.SetActive(false);
            
            onHide?.Invoke();

            return;
        }

        DOVirtual.Float(1, 0, 1, (value) => canvasGroup.alpha = value)
            .SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                root.gameObject.SetActive(false);
                onHide?.Invoke();
            });
    }
}

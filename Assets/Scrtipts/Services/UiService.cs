using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiService
{
    #region Fields

    private UiConfig uiConfig;
    private Transform uiRoot;

    private List<ScreenBase> screens;
    private ScreenType currentScreenType = ScreenType.None;

    private EventService eventService;

    #endregion



    #region Properties

    public EventService EventService => eventService;

    #endregion



    #region Public methods

    public UiService(EventService eventService)
    {
        this.eventService = eventService;

        uiConfig = GameManager.Instance.DataHub.UiConfig;
        uiRoot = GameManager.Instance.UiRoot;

        InitializeScreens();
    }


    public void ShowScreen(ScreenType screenType, Action onScreenShowed = null)
    {
        if (currentScreenType != ScreenType.None)
        {
            HideScreen(currentScreenType,
                () =>
                {
                    ShowScreen(screenType, onScreenShowed);
                });

            return;
        }

        ScreenBase screenBase = screens.Find(screen => screen.ScreenType == screenType);
        if (screenBase)
        {
            screenBase.Show(() =>
            {
                currentScreenType = screenType;
                onScreenShowed?.Invoke();
            });
        }
        else
        {
            Debug.LogError($"Screen of type: {screenType} not found!");
        }
    }


    public void HideScreen(ScreenType screenType, Action onScreenHidden = null)
    {
        if (screenType == ScreenType.None)
        {
            onScreenHidden?.Invoke();
            return;
        }

        ScreenBase screenBase = screens.Find(screen => screen.ScreenType == screenType);
        if (screenBase)
        {
            screenBase.Hide(false, () =>
            {
                currentScreenType = ScreenType.None;
                onScreenHidden?.Invoke();
            });
        }
        else
        {
            Debug.LogError($"Screen of type: {screenType} not found!");
        }
    }

    #endregion



    #region Private methods

    private void InitializeScreens()
    {
        screens = new List<ScreenBase>();

        foreach(var screenInfo in uiConfig.Screens)
        {
            ScreenBase screenBase = GameObject.Instantiate(screenInfo.screenPrefab, uiRoot);
            screenBase.Initialize(this);
            screenBase.Hide(immediately: true);

            screens.Add(screenBase);
        }

        ShowScreen(uiConfig.StartScreen);
    }

    #endregion
}

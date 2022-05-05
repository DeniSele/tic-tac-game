using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UiConfig", menuName = "Data/UI", order = 1)]
public class UiConfig : ScriptableObject
{
    #region Nested types

    [Serializable]
    public class ScreenInfo
    {
        public ScreenType screenType;
        public ScreenBase screenPrefab;
    }

    #endregion



    #region Fields

    [Header("Screens info")]

    [SerializeField] private List<ScreenInfo> screens;

    [Space]
    [SerializeField] private ScreenType startScreeen;

    #endregion



    #region Properties

    public List<ScreenInfo> Screens => screens;
    public ScreenType StartScreen => startScreeen;

    #endregion



    #region Public methods

    public ScreenBase GetScreenByType(ScreenType screenType)
    {
        return screens.Find(screenInfo => screenInfo.screenType == screenType).screenPrefab;
    }

    #endregion
}

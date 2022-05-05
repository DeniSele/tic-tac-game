using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHub
{
    private UiConfig uiConfig;
    private TicTacGameConfig ticTacGameConfig;
    private TicTacGameGraphicsConfig gameGraphicsConfig;


    public UiConfig UiConfig => uiConfig;
    public TicTacGameConfig TicTacGameConfig => ticTacGameConfig;
    public TicTacGameGraphicsConfig GameGraphicsConfig => gameGraphicsConfig;


    public DataHub()
    {
        uiConfig = GetLazy(ref uiConfig, "Data/Ui/UiConfig");
        ticTacGameConfig = GetLazy(ref ticTacGameConfig, "Data/TicTacGame/TicTacGameConfig");
        gameGraphicsConfig = GetLazy(ref gameGraphicsConfig, "Data/TicTacGame/GameGraphicsConfig");
    }


    private T GetLazy<T>(ref T backingStorage, string resourcePath) where T : ScriptableObject
    {
        if (backingStorage == null)
        {
            backingStorage = Resources.Load<T>(resourcePath);
        }

        return backingStorage;
    }
}

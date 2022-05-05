using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacGameService
{
    #region Fields

    private TicTacGameController gameController;
    private GameOption gameOption;

    #endregion



    #region Properties

    public TicTacGameController GameController => gameController;
    public GameOption GameOption => gameOption;

    #endregion



    #region Public methods

    public void Initialize(List<ICell> cells)
    {
        if(gameController != null)
        {
            gameController.SetPlayerControllers(GetPlayController(gameOption.Player1), GetPlayController(gameOption.Player2));
        }
        else
        {
            gameController = new TicTacGameController(
                GetPlayController(gameOption.Player1),
                GetPlayController(gameOption.Player2),
                cells);
        }
    }


    public void SetGameOption(GameOption option)
    {
        gameOption = option;
    }

    #endregion



    #region Private methods

    private IPlayController GetPlayController(PlayControllerType controllerType)
    {
        switch (controllerType)
        {
            case PlayControllerType.LocalPlayer:
                return new LocalPlayController();
            case PlayControllerType.ComputerPlayer:
                return new ComputerPlayController();
            default:
                Debug.LogError($"Player controller of type: {controllerType} not found! Local controller setted.");
                return new LocalPlayController();
        }
    }

    #endregion
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TicTacGameController
{
    #region Fields

    public const int CELLS_COUNT = 9;

    private IPlayController player1;
    private IPlayController player2;

    private IPlayController currentActivePlayer;

    private ServicesHub servicesHub;

    private TimeController timeController;
    private GameResultController resultController;
    private AdvancedFeaturesController featuresController;

    private List<ICell> cells;

    private GameResultType gameResult;

    #endregion



    #region Properties

    public List<ICell> Cells => cells;

    #endregion



    #region Public methods

    public TicTacGameController(IPlayController player1, IPlayController player2, List<ICell> cells)
    {
        servicesHub = GameManager.Instance.ServicesHub;

        SetPlayerControllers(player1, player2);

        this.cells = cells;

        timeController = new TimeController();
        timeController.OnTimerEnded += () => OnTimeEnded();

        resultController = new GameResultController();
        featuresController = new AdvancedFeaturesController(servicesHub.EventService);
    }


    public void SetPlayerControllers(IPlayController player1, IPlayController player2)
    {
        if(this.player1 != null)
        {
            this.player1.OnMoveCompleted -= OnMoveCompleted;
        }

        if(this.player2 != null)
        {
            this.player2.OnMoveCompleted -= OnMoveCompleted;
        }

        this.player1 = player1;
        this.player2 = player2;

        this.player1.OnMoveCompleted += OnMoveCompleted;
        this.player2.OnMoveCompleted += OnMoveCompleted;
    }


    public void Undo()
    {
        if (gameResult != GameResultType.None)
            return;

        featuresController.Undo();
    }


    public void Hint()
    {
        if (gameResult != GameResultType.None)
            return;

        currentActivePlayer.DisallowMove();

        var cellSetted = featuresController.Hint(cells);

        OnMoveCompleted(cellSetted);
    }


    public void StartGame()
    {
        if (currentActivePlayer != null)
            currentActivePlayer.DisallowMove();

        timeController.StopTimer();

        RandomizePlayerSign();

        featuresController.ResetSteps();

        gameResult = GameResultType.None;

        foreach (var cell in cells)
            cell.Reset();

        currentActivePlayer = player1;
        currentActivePlayer.AllowMove();

        timeController.StartTimer();
    }

    #endregion



    #region Private methods

    private void OnTimeEnded()
    {
        gameResult = currentActivePlayer != this.player1 ? GameResultType.Player1Win : GameResultType.Player2Win;

        timeController.StopTimer();
        currentActivePlayer.DisallowMove();

        InvokeResultScreen(gameResult);
    }


    private void RandomizePlayerSign()
    {
        int randomNumber = Random.Range(0, 2);

        IPlayController cachedPlayer1 = player1;
        IPlayController cachedPlayer2 = player2;

        player1 = randomNumber == 0 ? cachedPlayer1 : cachedPlayer2;
        player1.Initialize(GameSignType.Cross);

        player2 = randomNumber == 0 ? cachedPlayer2 : cachedPlayer1;
        player2.Initialize(GameSignType.Zero);
    }


    private void OnMoveCompleted(ICell cell)
    {
        if (cell == null)
            return;

        cell.SetSign(currentActivePlayer.SignType);

        if (resultController.CheckGameCompletion(cells, cell.CellSignType, out GameResultType gameResultLocal))
        {
            timeController.StopTimer();

            gameResult = gameResultLocal;
            InvokeResultScreen(gameResultLocal);

            return;
        }

        servicesHub.EventService.TicTacEvents.OnCellUpdated?.Invoke(cell);

        currentActivePlayer = currentActivePlayer == player1 ? player2 : player1;
        currentActivePlayer.AllowMove();

        timeController.StartTimer();
    }


    private void InvokeResultScreen(GameResultType gameResult)
    {
        DOVirtual.DelayedCall(1, () =>
        {
            servicesHub.EventService.TicTacEvents.OnGameEnded?.Invoke(gameResult);
            servicesHub.UiService.ShowScreen(ScreenType.ResultScreen);
        });
    }

    #endregion
}

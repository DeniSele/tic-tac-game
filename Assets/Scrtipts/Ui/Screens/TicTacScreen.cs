using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacScreen : ScreenBase
{
    #region Fields

    [Header("TicTac screen settings")]
    [SerializeField] private Transform cellsRoot;
    [SerializeField] private Cell prefab;
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text timeLabel;

    [Space]
    [SerializeField] private Button undoButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button restartButton;

    private TicTacGameService gameService;
    private DataHub dataHub;
    private List<ICell> cells;

    #endregion



    #region Public methods

    public override void Initialize(UiService uiService)
    {
        base.Initialize(uiService);

        dataHub = GameManager.Instance.DataHub;
        uiService.EventService.TicTacEvents.OnTimeUpdated += (float time) => timeLabel.text = string.Format("{0:0.0}", time);
    }


    public override void Show(Action onShow = null)
    {
        base.Show(onShow);

        background.sprite = dataHub.GameGraphicsConfig.GetBackgroundSprite();

        InitializeCells();

        gameService = GameManager.Instance.ServicesHub.TicTacGameService;
        gameService.Initialize(cells);
        gameService.GameController.StartGame();

        InitializeButtons();
    }


    public override void Hide(bool immediately = false, Action onHide = null)
    {
        DeinitializeButtons();

        base.Hide(immediately, onHide);
    }

    #endregion



    #region Private methods

    private void InitializeButtons()
    {
        restartButton.onClick.AddListener(gameService.GameController.StartGame);

        undoButton.onClick.AddListener(gameService.GameController.Undo);
        undoButton.gameObject.SetActive(gameService.GameOption.IsUndoOptionEnabled);

        hintButton.onClick.AddListener(gameService.GameController.Hint);
        hintButton.gameObject.SetActive(gameService.GameOption.IsHintOptionEnabled);
    }


    private void DeinitializeButtons()
    {
        restartButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();
        hintButton.onClick.RemoveAllListeners();
    }

    private void InitializeCells()
    {
        if (cells != null)
            return;

        cells = new List<ICell>();
        for(int i = 0; i < TicTacGameController.CELLS_COUNT; i++)
        {
            ICell cell = Instantiate(prefab, cellsRoot);
            cell.Initialize(i);
            cells.Add(cell);
        }
    }

    #endregion
}

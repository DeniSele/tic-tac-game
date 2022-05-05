using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayController : IPlayController
{
    public Action<ICell> OnMoveCompleted { get; set; }

    public GameSignType SignType => signType;
    private GameSignType signType;

    private bool isMoveAllowed;


    public void Initialize(GameSignType signType)
    {
        this.signType = signType;
        isMoveAllowed = false;

        GameManager.Instance.ServicesHub.EventService.TicTacEvents.OnCellClicked += OnCellClicked;
    }

    public void AllowMove()
    {
        isMoveAllowed = true;
    }


    public void DisallowMove()
    {
        isMoveAllowed = false;
    }


    private void OnCellClicked(ICell cell)
    {
        if (!isMoveAllowed)
            return;

        if(cell.CellSignType == GameSignType.None)
        {
            isMoveAllowed = false;

            OnMoveCompleted?.Invoke(cell);
        }
    }
}

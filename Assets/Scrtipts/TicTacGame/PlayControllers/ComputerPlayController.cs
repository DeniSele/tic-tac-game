using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayController : IPlayController
{
    public Action<ICell> OnMoveCompleted { get; set ; }


    public GameSignType SignType => signType;

    private GameSignType signType;

    private Tween stepTween;


    public void Initialize(GameSignType signType)
    {
        this.signType = signType;
    }


    public void AllowMove()
    {
        List<ICell> cells = GameManager.Instance.ServicesHub.TicTacGameService.GameController.Cells
                .FindAll(cell => cell.CellSignType == GameSignType.None);

        int index = UnityEngine.Random.Range(0, cells.Count);
        var cellSetted = cells[index];

        stepTween = DOVirtual.DelayedCall(UnityEngine.Random.Range(1, 3), () => OnMoveCompleted?.Invoke(cellSetted));
    }


    public void DisallowMove()
    {
        if (stepTween != null)
            stepTween.Kill();
    }
}

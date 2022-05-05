using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameResultController
{
    private readonly List<Vector3Int> WINNING_COMBINATIONS = new List<Vector3Int>()
    {
        new Vector3Int(0, 1, 2),
        new Vector3Int(3, 4, 5),
        new Vector3Int(6, 7, 8),
        new Vector3Int(0, 3, 6),
        new Vector3Int(1, 4, 7),
        new Vector3Int(2, 5, 8),
        new Vector3Int(0, 4, 8),
        new Vector3Int(2, 4, 6)
    };


    public bool CheckGameCompletion(List<ICell> cells, GameSignType signType, out GameResultType gameResult)
    {
        foreach (var winningCombination in WINNING_COMBINATIONS)
        {
            if (IsWinnigCombinationCompleted(cells, winningCombination, signType))
            {
                gameResult = signType == GameSignType.Cross ? GameResultType.Player1Win : GameResultType.Player2Win;
                return true;
            }
        }

        int movesCount = cells.Count(cell => cell.CellSignType != GameSignType.None);
        if (movesCount == cells.Count)
        {
            gameResult = GameResultType.Draw;
            return true;
        }

        gameResult = GameResultType.None;
        return false;
    }


    private bool IsWinnigCombinationCompleted(List<ICell> cells, Vector3Int cellPos, GameSignType signType)
    {
        return cells[cellPos.x].CellSignType == signType &&
            cells[cellPos.y].CellSignType == signType &&
            cells[cellPos.z].CellSignType == signType;
    }
}

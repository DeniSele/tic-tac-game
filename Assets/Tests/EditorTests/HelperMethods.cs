using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMethods
{
    public static List<ICell> InitializeCells(List<int> initData)
    {
        List<ICell> cells = new List<ICell>();

        for (int i = 0; i < initData.Count; i++)
        {
            var cell = new TestCell();
            cell.Initialize(i);
            cell.SetSign((GameSignType)initData[i]);
            cells.Add(cell);
        }

        return cells;
    }


    public static List<int> FromCellsToInitData(List<ICell> cells)
    {
        List<int> initData = new List<int>();

        for (int i = 0; i < cells.Count; i++)
        {
            initData.Add((int)cells[i].CellSignType);
        }

        return initData;
    }
}

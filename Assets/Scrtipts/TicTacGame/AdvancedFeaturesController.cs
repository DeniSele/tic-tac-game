using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedFeaturesController
{
    #region Fields

    private Stack<ICell> steps;

    #endregion



    #region Public methods

    public AdvancedFeaturesController(EventService eventService)
    {
        steps = new Stack<ICell>();

        eventService.TicTacEvents.OnCellUpdated += OnCellUpdated;
    }


    public void ResetSteps()
    {
        steps.Clear();
    }


    public void Undo()
    {
        if (steps.Count <= 1)
            return;

        ICell cell = steps.Pop();
        cell.Reset();

        cell = steps.Pop();
        cell.Reset();
    }


    public ICell Hint(List<ICell> cells)
    {
        return cells.Find(cell => cell.CellSignType == GameSignType.None);
    }

    #endregion



    #region Private methods

    private void OnCellUpdated(ICell cell)
    {
        steps.Push(cell);
    }

    #endregion
}

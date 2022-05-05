using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public class GameEvents
    {
        public Action<GameResultType> OnGameEnded;
        
        public Action<ICell> OnCellClicked;
        public Action<ICell> OnCellUpdated;

        public Action<float> OnTimeUpdated;
    }


    public GameEvents TicTacEvents;


    public EventService()
    {
        TicTacEvents = new GameEvents();
    }
}

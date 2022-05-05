using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicesHub
{
    private UiService uiService;
    private TicTacGameService gameService;
    private EventService eventService;


    public UiService UiService => uiService;
    public TicTacGameService TicTacGameService => gameService;
    public EventService EventService => eventService;


    public ServicesHub()
    {
        eventService = new EventService();
        uiService = new UiService(eventService);
        gameService = new TicTacGameService();
    }
}

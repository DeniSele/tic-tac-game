using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCell : ICell
{
    public int Index => index;
    private int index;

    public GameSignType CellSignType => gameSign;
    private GameSignType gameSign;


    public void Initialize(int index)
    {
        this.index = index;
    }


    public void Reset()
    {
        gameSign = GameSignType.None;
    }


    public void SetSign(GameSignType gameSign)
    {
        this.gameSign = gameSign;
    }
}

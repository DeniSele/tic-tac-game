using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameOption
{
    [Header("Game Options")]
    [SerializeField] private string optionName;

    [Space]
    [SerializeField] private PlayControllerType player1;
    [SerializeField] private PlayControllerType player2;

    [Space]
    [SerializeField] private bool isUndoOptionEnabled = false;
    [SerializeField] private bool isHintOptionEnabled = false;

    public string OptionName => optionName;
    public PlayControllerType Player1 => player1;
    public PlayControllerType Player2 => player2;

    public bool IsUndoOptionEnabled => isUndoOptionEnabled;
    public bool IsHintOptionEnabled => isHintOptionEnabled;
}

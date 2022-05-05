using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TicTacGameConfig", menuName = "Data/Game", order = 1)]
public class TicTacGameConfig : ScriptableObject
{
    [Header("Main info")]
    [SerializeField] private float moveTime;

    [Space]
    [SerializeField] private List<GameOption> options;

    public float MoveTime => moveTime;
    public List<GameOption> Options => options;
}

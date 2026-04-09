using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Idle,
    Move,
    Dash
}

public class StateMachine : MonoBehaviour
{
    public GameState gameState;

    public void SetGameState(GameState state)
    {
        gameState = state;
    }
}

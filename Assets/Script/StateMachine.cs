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
    GameState curState;

    public void SetGameState(GameState state)
    {
        curState = state;

        switch (state)
        {
            case GameState.Idle:
                break;
            
            case GameState.Dash:
                break;
            
            case GameState.Move:
                break;
        }
    }
}

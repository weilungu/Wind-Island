using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Dashing
}

public class StateMachine : MonoBehaviour
{
    GameState curState;

    public void SetGameState(GameState state)
    {
        curState = state;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Move,
    Dash
}

public class StateMachine : MonoBehaviour
{
    public PlayerState playerState;

    public void SetGameState(PlayerState state)
    {
        playerState = state;

        switch (state)
        {
            case PlayerState.Idle:
                break;
            
            case PlayerState.Move:
                break;
            
            case PlayerState.Dash:
                break;
        }
    }
}

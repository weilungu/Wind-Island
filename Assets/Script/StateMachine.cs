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
    PlayerState curState;
    
    [SerializeField] PlayerController player;

    public void SetGameState(PlayerState state)
    {
        curState = state;

        switch (state)
        {
            case PlayerState.Idle:
                break;
            
            case PlayerState.Move:
                break;
            
            case PlayerState.Dash:
                player.move.enabled = false;
                player.dash.TryDash();
                player.move.enabled = true;
                break;
        }
    }
}

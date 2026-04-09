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

public enum EnemyState
{
    Idle,
    Move,
    Dash
}

public class StateMachine : MonoBehaviour
{
    public PlayerState playerState;
    public EnemyState enemyState;

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
    public void SetGameState(EnemyState state)
    {
        enemyState = state;

        switch (state)
        {
            case EnemyState.Idle:
                break;
            
            case EnemyState.Move:
                break;
            
            case EnemyState.Dash:
                break;
        }
    }
}

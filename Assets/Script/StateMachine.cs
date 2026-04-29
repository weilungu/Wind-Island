using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Attack,
    Dead,
}
public enum EnemyState
{
    Idle,
    Chase,
    Dash,
    Attack,
    Dead,
}
public enum GameState
{
    InGame,
    Paused,
    GameOver,
    Quit,
}


public class StateMachine : MonoBehaviour
{
    public PlayerState playerState;
    public GameState gameState;

    public void SetGameState(PlayerState state)
    {
        playerState = state;
    }
    public void SetGameState(GameState state)
    {
        gameState = state;
    }
}

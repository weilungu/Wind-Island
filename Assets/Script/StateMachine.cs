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
    Hurt,
    Dead,
}
public enum EnemyState
{
    Idle,
    Chase,
    Dash,
    Attack,
    Hurt,
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
    public EnemyState enemyState;
    public GameState gameState;

    public void SetGameState(PlayerState state)
    {
        playerState = state;
    }
    public void SetGameState(EnemyState state)
    {
        enemyState = state;
    }
    public void SetGameState(GameState state)
    {
        gameState = state;
    }
}

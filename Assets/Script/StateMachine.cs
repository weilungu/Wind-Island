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
    [SerializeField] DashController dash;
    
    public void SetGameState(GameState state)
    {
        curState = state;

        switch (curState)
        {
            case GameState.Dashing:
                dash.Dash();
                break;
        }
    }
}

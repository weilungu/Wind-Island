using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputController inp;
    
    MoveController move;
    DashController dash;
    
    [SerializeField] StateMachine fsm;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        
        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
    }

    private void Start()
    {
        fsm.SetGameState(GameState.Idle);
    }

    void Update()
    {
        PlayerAction();
    }
    
    
    // Self-Methods
    void PlayerAction()
    {
        switch (fsm.gameState)
        {
            case GameState.Idle:
                if (inp.movePressed)
                {
                    fsm.SetGameState(GameState.Move);
                }
                
                break;
        
            
            case GameState.Move:
                move.Movement();
        
                if (inp.moveDirection.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                }
                else if (inp.dashPressed)
                {
                    dash.TryDash();
                }
                
                break;
            
            
            case GameState.Dash:
                break;
        }
    }
}

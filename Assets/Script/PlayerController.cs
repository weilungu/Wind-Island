using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputController inp;
    PlayerState prevState;
    
    [HideInInspector] public MoveController move;
    [HideInInspector] public DashController dash;
    
    [SerializeField] StateMachine fsm;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        
        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
    }

    private void Start()
    {
        fsm.SetGameState(PlayerState.Idle);
        prevState = fsm.curState;
    }

    void Update()
    {
        if (fsm.curState != prevState)
        {
            OnStateEnter(fsm.curState);
            prevState = fsm.curState;
        }

        switch (fsm.curState)
        {
            case PlayerState.Idle:
                if (inp.movePressed)
                {
                    fsm.SetGameState(PlayerState.Move);
                }
                
                break;

            
            case PlayerState.Move:
                move.Movement();

                if (inp.moveDirection.Equals(Vector2.zero))
                {
                    fsm.SetGameState(PlayerState.Idle);
                }
                else if (inp.dashPressed)
                {
                    fsm.SetGameState(PlayerState.Dash);
                }
                
                break;
            
            
            case PlayerState.Dash:
                if (!dash.IsDashing)
                {
                    fsm.SetGameState(inp.moveDirection.Equals(Vector2.zero) ? PlayerState.Idle : PlayerState.Move);
                }
                break;
        }
    }

    void OnStateEnter(PlayerState state)
    {
        if (state != PlayerState.Dash)
        {
            return;
        }

        bool started = dash.TryDash();
        if (!started)
        {
            fsm.SetGameState(inp.moveDirection.Equals(Vector2.zero) ? PlayerState.Idle : PlayerState.Move);
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputController inp;
    
    [HideInInspector] public MoveController move;
    [HideInInspector] public DashController dash;
    
    [SerializeField] StateMachine fsm;
    
    [SerializeField] PlayerState state;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        
        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
    }

    private void Start()
    {
        state = PlayerState.Idle;
        fsm.SetGameState(PlayerState.Idle);
    }

    void Update()
    {
            switch (state)
            {
                case PlayerState.Idle:
                    if (inp.movePressed)
                    {
                        state = PlayerState.Move;
                    }
                    
                    break;

                
                case PlayerState.Move:
                    move.Movement();

                    if (inp.moveDirection.Equals(Vector2.zero))
                    {
                        state = PlayerState.Idle;
                    }
                    else if (inp.dashPressed)
                    {
                        state = PlayerState.Dash;
                    }
                    
                    break;
                
                
                case PlayerState.Dash:
                    // dash.TryDash();
                    break;
            }
        

        if (inp.dashPressed)
        {
            fsm.SetGameState(PlayerState.Dash);
        }
    }
    
}

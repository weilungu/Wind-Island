using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    float horizontal;
    float vertical;
    Vector2 direction = Vector2.zero;
    
    // Instance
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
        PlayerActionState();
        
        if (inp.dashPressed)
        {
            dash.TryDash(direction);
        }
        
        print(direction);
    }
    void FixedUpdate()
    {
        MoveState();
    }
    
    
    // Self-Methods
    void PlayerActionState()
    {
        switch (fsm.gameState)
        {
            case GameState.Idle:
                if (inp.movePressed)
                {
                    fsm.SetGameState(GameState.Move);
                }
                break;
            
            case GameState.Dash:
                break;
        }
    }

    void MoveState()
    {
        switch (fsm.gameState)
        {
            case GameState.Move:
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
                
                direction = new Vector2(horizontal, vertical).normalized;
                move.Movement(direction);
                
                if (direction.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                }
                break;
        }
    }
}

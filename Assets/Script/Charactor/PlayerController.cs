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
    Animator anim;
    
    MoveController move;
    DashController dash;
    
    [SerializeField] StateMachine fsm;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        anim = GetComponent<Animator>();
        
        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
    }
    private void Start()
    {
        fsm.SetGameState(GameState.Idle);
        anim.SetBool(AnimParams.Idle, true);
        anim.SetBool(AnimParams.Move, false);
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
                anim.SetBool(AnimParams.Idle, true);
                anim.SetBool(AnimParams.Move, false);
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
                anim.SetBool(AnimParams.Move, true);
                anim.SetBool(AnimParams.Idle, false);
                
                
                if (direction.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                }
                break;
        }
    }
}

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
    SpriteRenderer sprite;

    MoveController move;
    DashController dash;
    Health health;

    [SerializeField] StateMachine fsm;

    private void Awake()
    {
        inp = GetComponent<InputController>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
        health = GetComponent<Health>();
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
        MoveState();

        if (inp.dashPressed)
        {
            dash.TryDash(direction);
        }
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

                if (inp.attackPressed)
                {
                    fsm.SetGameState(GameState.Attack);
                }
                break;
            
            
            case GameState.Attack:
                anim.SetTrigger(AnimParams.Attack);

                fsm.SetGameState(GameState.Move);
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

                if (direction != Vector2.zero) // 方向改變
                {
                    sprite.flipX = direction.x < 0;
                }
                
                anim.SetBool(AnimParams.Move, true);
                anim.SetBool(AnimParams.Idle, false);
                
                if (direction.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                }
                
                if (inp.attackPressed)
                {
                    fsm.SetGameState(GameState.Attack);
                }

                break;
        }
    }
}
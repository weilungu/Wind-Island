using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector2 direction = Vector2.zero;
    Vector2 nonZeroDir = Vector2.right;

    // Instance
    Animator anim;
    SpriteRenderer sprite;

    InputController inp;
    MoveController move;
    DashController dash;
    AttackController attack;
    Health health;

    [Header("Field Instance")]
    [SerializeField] StateMachine fsm;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
        health = GetComponent<Health>();
        attack = GetComponent<AttackController>();
    }

    private void Start()
    {
        fsm.SetGameState(GameState.Idle);
        anim.SetBool(AnimParams.Idle, true);
        anim.SetBool(AnimParams.Move, false);
        anim.SetBool(AnimParams.IdleUp, false);
        anim.SetBool(AnimParams.IdleDown, false);
        anim.SetBool(AnimParams.MoveUp, false);
        anim.SetBool(AnimParams.MoveDown, false);
    }

    void Update()
    {
        inp.MoveInput(ref horizontal, ref vertical);
        
        PlayerActionState();

        if (fsm.gameState.Equals(GameState.Move))
        {
            if (inp.attackPressed)
            {
                fsm.SetGameState(GameState.Attack);
            }
        }
        
        if (inp.dashPressed)
        {
            dash.TryDash(direction);
        }
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
                // Animation
                anim.SetBool(AnimParams.Idle, true);
                anim.SetBool(AnimParams.Move, false);

                if (nonZeroDir.y > 0.01f)
                {
                    anim.SetBool(AnimParams.IdleUp, true);
                }
                else if (nonZeroDir.y < -0.01f)
                {
                    anim.SetBool(AnimParams.IdleDown, true);
                }

                // Transition
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
                if (attack.canAttack)
                {
                    anim.SetTrigger(AnimParams.Attack);
                }

                attack.TryAttack(direction);

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
                // Direction
                direction = new Vector2(horizontal, vertical).normalized;
                if (!direction.Equals(Vector2.zero)) nonZeroDir = direction;
                
                attack.UpdateAttackDirection(direction);
                move.Movement(direction);
                
                if (direction.x < 0)
                {
                    sprite.flipX = true;
                }
                else if (direction.x > 0)
                {
                    sprite.flipX = false;
                }
                
                // Animation
                anim.SetBool(AnimParams.Move, true);
                anim.SetBool(AnimParams.Idle, false);
                anim.SetBool(AnimParams.IdleUp, false);
                anim.SetBool(AnimParams.IdleDown, false);

                if (direction.y > 0.01f) // Move Up
                {
                    anim.SetBool(AnimParams.MoveUp, true);
                    anim.SetBool(AnimParams.MoveDown, false);
                }
                else if (direction.y < -0.01f) // Move Down
                {
                    anim.SetBool(AnimParams.MoveDown, true);
                    anim.SetBool(AnimParams.MoveUp, false);
                }
                else
                {
                    anim.SetBool(AnimParams.MoveUp, false);
                    anim.SetBool(AnimParams.MoveDown, false);
                }
                
                // Transition
                if (direction.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                }
                break;
        }
    }
}
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
    
    void Awake()
    {
        inp = GetComponent<InputController>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
        health = GetComponent<Health>();
        attack = GetComponent<AttackController>();
    }

    void Start()
    {
        fsm.SetGameState(GameState.Idle);
        
        anim.SetFloat(AnimParams.MoveX, 0f);
        anim.SetFloat(AnimParams.MoveY, 0f);
        anim.SetBool(AnimParams.IsMoving, false);
    }

    void Update()
    {
        PlayerActionState();
        InputHandler();
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

                // Anim
                SetMoveAnim(false);

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
                    attack.TryAttack(nonZeroDir);
                    attack.UpdateAttackDirection(nonZeroDir);
                }
                else
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
                // Direction
                direction = new Vector2(horizontal, vertical).normalized;
                if (!direction.Equals(Vector2.zero)) nonZeroDir = direction;
                
                attack.UpdateAttackDirection(direction);
                move.Movement(direction);
                
                if (direction.x != 0)
                {
                    sprite.flipX = direction.x < 0;
                }
                
                // Animation
                SetMoveAnim(true);
                
                // Transition
                if (direction.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                    SetMoveAnim(false);
                }
                break;
        }
    }


    void SetMoveAnim(bool isMoving)
    {
        anim.SetFloat(AnimParams.MoveX, nonZeroDir.x);
        anim.SetFloat(AnimParams.MoveY, nonZeroDir.y);
        anim.SetBool(AnimParams.IsMoving, isMoving);
    }

    void InputHandler()
    {
        inp.MoveInput(ref horizontal, ref vertical);

        if (fsm.gameState.Equals(GameState.Move))
        {
            if (inp.attackPressed)
            {
                SetMoveAnim(true);
                fsm.SetGameState(GameState.Attack);
            }
        }
        
        if (inp.dashPressed)
        {
            dash.TryDash(direction);
        }
    }
}
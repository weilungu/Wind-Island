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
    Animator anim;
    SpriteRenderer sprite;

    InputController inp;
    MoveController move;
    DashController dash;
    AttackController attack;
    Health health;

    [Header("Field Instance")]
    [SerializeField] StateMachine fsm;
    
    [Header("Values")]
    [SerializeField] int ATK;
    
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
    }

    void Update()
    {
        inp.MoveInput(ref horizontal, ref vertical);
        
        PlayerActionState();

        if (fsm.gameState.Equals(GameState.Move))
        {
            if (inp.attackPressed && attack.canAttack)
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
                anim.SetBool(AnimParams.Idle, true);
                anim.SetBool(AnimParams.Move, false);
                
                if (inp.movePressed)
                {
                    fsm.SetGameState(GameState.Move);
                }

                if (inp.attackPressed && attack.canAttack)
                {
                    fsm.SetGameState(GameState.Attack);
                }
                break;
            
            
            case GameState.Attack:
                anim.SetTrigger(AnimParams.Attack);

                attack.TryAttack(ATK);
                
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

                direction = new Vector2(horizontal, vertical).normalized;
                move.Movement(direction);
                
                if (direction.x < 0)
                {
                    sprite.flipX = true;
                }
                else if (direction.x > 0)
                {
                    sprite.flipX = false;
                }
                
                anim.SetBool(AnimParams.Move, true);
                anim.SetBool(AnimParams.Idle, false);
                
                if (direction.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                }
                
                break;
        }
    }

    // void Attack()
    // {
    //     // Detect Enemy in range
    //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    //     
    //     // Damage
    //     foreach (Collider2D enemy in hitEnemies)
    //     {
    //         print($"we hit {enemy.name}");
    //     }
    // }

    // void OnDrawGizmosSelected()
    // {
    //     if (attackPoint.Equals(null)) return;
    //     
    //     Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    // }
}
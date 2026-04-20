using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    MoveController move;
    AttackController attack;
    StateMachine fsm;
    
    [SerializeField] Transform target;
    
    Vector2 faceDir = Vector2.zero;

    [Header("Debug")]
    [SerializeField] bool hasPlayerInFront;

    void Awake()
    {
        move = GetComponent<MoveController>();
        attack = GetComponent<AttackController>();
        fsm = GetComponent<StateMachine>();
    }

    public void SetTarget(Transform t)
    {
        target = t;
        // isChasing = true;
    }
    public void ClearTarget()
    {
        target = null;
        // isChasing = false;
    }

    void FixedUpdate()
    {
        

        // Chase();
        //
        // if (!attack.Equals(null))
        // {
        //     hasPlayerInFront = attack.TryGetPlayerInFront(out _);
        // }
        //
        // if (hasPlayerInFront)
        // {
        //     print("Player In Front");
        // }
        
        EnemyAttack();
    }

    void Chase()
    {
        faceDir = (target.position - transform.position).normalized;

        if (target.Equals(null))
        {
            hasPlayerInFront = false;
            return;
        }
        if (!attack.Equals(null))
        {
            attack.UpdateAttackDirection(faceDir);
        }
        
        move.Move(faceDir);
    }

    void EnemyAttack()
    {
        Chase();

        if (!attack.Equals(null))
        {
            hasPlayerInFront = attack.TryGetPlayerInFront(out _);
        }

        if (hasPlayerInFront)
        {
            print("Player In Front");
            attack.TryAttack(faceDir);
        }
    }
}

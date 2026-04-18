using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyMovement move;
    
    Transform target = null;
    bool isChasing = false;
    
    Vector2 faceDir = Vector2.zero;

    void Awake()
    {
        move = GetComponent<EnemyMovement>();
    }

    public void SetTarget(Transform t)
    {
        target = t;
        isChasing = true;
    }
    public void ClearTarget()
    {
        target = null;
        isChasing = false;
    }

    void FixedUpdate()
    {
        if (isChasing && !target.Equals(null))
        {
            if (move.isBlocked) return;
            
            print($"Player is in range");
            Chase();
        }
    }

    void Chase()
    {
        faceDir = (target.position - transform.position).normalized;
        move.Move(faceDir);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    MoveController move;
    
    [SerializeField] Transform target;
    // bool isChasing = false;
    
    Vector2 faceDir = Vector2.zero;

    void Awake()
    {
        move = GetComponent<MoveController>();
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
        // if (isChasing && !target.Equals(null))
        // {
        //     print($"Player is in range");
        //     Chase();
        // }
        
        Chase();
    }

    void Chase()
    {
        faceDir = (target.position - transform.position).normalized;
        
        move.Move(faceDir);
    }
}

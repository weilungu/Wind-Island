using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] LayerMask obstacleLayer;
    public bool isBlocked = false;
    
    public override void Move(Vector2 direction)
    {
        float f_dt = Time.fixedDeltaTime * speed;
        transform.Translate(direction * f_dt);
    }
}

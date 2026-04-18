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

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(((1 << other.gameObject.layer) & obstacleLayer));
        if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            isBlocked = true;
            // speed = 0;
        }
    }
}

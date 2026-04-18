using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    // [SerializeField] float speed;
    
    public override void Move(Vector2 direction)
    {
        float f_dt = Time.fixedDeltaTime * speed;
        transform.Translate(direction * f_dt);
        
        // base.Move(direction);
    }
}

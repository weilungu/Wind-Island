using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Instance
    Rigidbody2D rb;
    
     [SerializeField] protected float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public new virtual void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float f_dt = Time.fixedDeltaTime * speed;

            Vector2 pos = rb.position + direction * f_dt;
            rb.MovePosition(pos);
            // transform.Translate(direction * speed * dt);
        }
    }
}
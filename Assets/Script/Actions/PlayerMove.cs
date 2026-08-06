using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    
    [Header("Values")]
    [SerializeField] float speed = 1f;

    // === Unity Life Cycle === //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    // === Self API === //
    public void Move(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }
    
    public void StopMove() => rb.velocity = Vector2.zero;
}

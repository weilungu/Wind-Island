using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Awake Values
    Rigidbody2D rb;
    
    [Header("Inspector Values")]
    [SerializeField] float moveSpeed = 1f;
    
    // Private Values
    [Header("Show")]
    [SerializeField] Vector2 dir;
    
    
    // == Unity API == //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // == Self API == //
    public void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        dir = new Vector2(x, y).normalized;
        rb.velocity = dir * moveSpeed;
    }

    public Vector2 Direction => dir;
}

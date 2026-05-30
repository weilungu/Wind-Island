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

    void FixedUpdate()
    {
        dir = Direction;
    }


    // == Self API == //
    public void Movement()
    {
        rb.velocity = Direction * moveSpeed;
    }

    public static Vector2 Direction =>
        new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
}

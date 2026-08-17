using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerGuardBreak guardBreak;
    
    [Header("Values")]
    [SerializeField] float moveSpeed = 1f;
    
    // Private Values
    float gbSpeed;

    // === Unity Life Cycle === //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        guardBreak = GetComponent<PlayerGuardBreak>();
    }

    void Start()
    {
        gbSpeed = moveSpeed * (guardBreak.SpeedPercent / 100);
    }

    
    // === Self API === //
    [HideInInspector] public int MoveX = Animator.StringToHash("MoveX");
    [HideInInspector] public int MoveY = Animator.StringToHash("MoveY");
    
    public void Move(Vector2 dir)
    {
        if (!guardBreak.IsGuardBreak)
        {
            rb.velocity = dir * moveSpeed;
            return;
        }
        
        rb.velocity = dir * gbSpeed;
    }
    public void StopMove() => rb.velocity = Vector2.zero;
}

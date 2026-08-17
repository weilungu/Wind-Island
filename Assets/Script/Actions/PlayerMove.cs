using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerInput input;
    
    Rigidbody2D rb;
    Animator anim;
    
    PlayerGuardBreak guardBreak;
    
    [Header("Values")]
    [SerializeField] float moveSpeed = 1f;
    
    // Private Values
    float gbSpeed;
    
    int MoveX = Animator.StringToHash("MoveX");
    int MoveY = Animator.StringToHash("MoveY");

    
    // === Unity Life Cycle === //
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
        guardBreak = GetComponent<PlayerGuardBreak>();
    }

    void Start()
    {
        gbSpeed = moveSpeed * (guardBreak.SpeedPercent / 100);
    }

    
    // === Self API === //
    public Vector2 Direction => new Vector2(input.Horizontal, input.Vertical).normalized;
    
    public void Movement()
    {
        if (guardBreak.IsGuardBreak)
        {
            rb.velocity = Direction * gbSpeed;
            return;
        }
        
        rb.velocity = Direction * moveSpeed;
    }
    public void StopMove() => rb.velocity = Vector2.zero;

    public void PlayMoveAnimation()
    {
        anim.SetFloat(MoveX, Direction.x);
        anim.SetFloat(MoveY, Direction.y);
    }
}

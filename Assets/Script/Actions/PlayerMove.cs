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
        gbSpeed = moveSpeed * (guardBreak.SpeedPCT / 100);
    }

    
    // === Self API === //
    public Vector2 Direction => new Vector2(input.Horizontal, input.Vertical).normalized;
    public Vector2 facingDirection
    {
        get
        {
            Vector2 dir = Vector2.right;
            if (Direction == Vector2.zero) return dir;
            
            // if Direction != 0
            dir = Direction;
            return Direction;
        }
    }
    
    public void Move() => rb.velocity = Direction * moveSpeed;
    public void GuardBreakMove() => rb.velocity = Direction * gbSpeed;
    
    public void StopMove() => rb.velocity = Vector2.zero;

    
    public void MoveAnimation()
    {
        anim.SetFloat(MoveX, Direction.x);
        anim.SetFloat(MoveY, Direction.y);
    }
}

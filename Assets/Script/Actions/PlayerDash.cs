using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Awake Values
    Rigidbody2D rb;
    PlayerMove move;
    
    
    [Header("Dash Values")]
    [SerializeField] float dashSpeed = 12f;
    [SerializeField] float dashTime = 0.5f;
    [SerializeField] float dashCooldown = 0.2f;

    [Space]
    [SerializeField] int postureIncrease = 10;
    
    [Header("Debug Show")]
    public bool canDash = true;
    public bool isDashing = false;
    
    // Private Values
    Vector2 dashDir;
    
    
    // === Unity Life Cycle === //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<PlayerMove>();
    }

    
    // === Self API === //
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        dashDir = move.Direction;

        rb.velocity = dashDir * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        
        canDash = true;
    }

    public void TryDash()
    {
        if (!canDash || isDashing) return;
        
        StartCoroutine(Dash());
    }
    
    public int GetPosture() => postureIncrease;
}

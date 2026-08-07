using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Awake Values
    Rigidbody2D rb;
    New_PlayerController player;
    
    [Header("Show")]
    public bool canDash = true;
    public bool isDashing = false;
    
    [Header("Inspector Values")]
    [SerializeField] float dashSpeed = 12f;
    [SerializeField] float dashTime = 0.5f;
    [SerializeField] float dashCooldown = 0.2f;

    // Private Values
    Vector2 dashDir;
    
    
    // === Unity Life Cycle === //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<New_PlayerController>();
    }

    
    // === Self API === //
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        dashDir = player.MoveDirection;

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
}

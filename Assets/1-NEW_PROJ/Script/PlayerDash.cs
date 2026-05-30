using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Awake
    Rigidbody2D rb;
    
    [Header("Inspector Values")]
    [SerializeField] float dashSpeed = 1f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    
    // Private Values
    [Header("Show")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing = false;
    
    
    // == Unity API == //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    
    // == Self API == //
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(DashRoutine());
        }
    }

    public IEnumerator DashRoutine()
    {
        Vector2 dashDir = PlayerMovement.Direction;
        if (dashDir == Vector2.zero) yield break;
        
        print("Dash started");
        canDash = false;
        isDashing = true;

        // Dashing
        rb.velocity = dashDir * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        
        // Reset State
        rb.velocity = Vector2.zero;
        isDashing = false;
        
        
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        print("Dash finished");
    }
    
    public bool IsDashing => isDashing;
    public bool CanDash => canDash;
}

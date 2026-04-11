using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] bool canDash;
    [SerializeField] bool isDashing;

    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    
    // Instance
    Rigidbody2D rb;
    InputController inp;
    [SerializeField] StateMachine fsm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inp = GetComponent<InputController>();
    }

    IEnumerator DashRoutine(Vector2 direction)
    {
        canDash = false;
        rb.velocity = direction.normalized * dashSpeed;
        
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;

        isDashing = false;
        
        
        if (! direction.Equals(Vector2.zero)) // 依然移動中
        {
            fsm.SetGameState(GameState.Move);
        }
        else
        {
            fsm.SetGameState(GameState.Idle);
        }

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
    
    public bool TryDash(Vector2 direction)
    {
        // Vector2 moveDir = inp.moveDirection;
        if (isDashing || !canDash || direction.Equals(Vector2.zero))
        {
            return false;
        }

        StartCoroutine(DashRoutine(direction));
        fsm.SetGameState(GameState.Dash);
        return true;
    }
}
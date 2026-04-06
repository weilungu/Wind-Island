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
        
        
        if (! inp.moveDirection.Equals(Vector2.zero)) // 依然移動中
        {
            fsm.SetGameState(PlayerState.Move);
        }
        else
        {
            fsm.SetGameState(PlayerState.Idle);
        }

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
    
    public bool TryDash()
    {
        Vector2 moveDir = inp.moveDirection;
        if (isDashing || !canDash || moveDir.Equals(Vector2.zero))
        {
            return false;
        }

        StartCoroutine(DashRoutine(moveDir));
        fsm.SetGameState(PlayerState.Dash);
        return true;
    }
}
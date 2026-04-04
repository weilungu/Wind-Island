using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] bool canDash = true;
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
        isDashing = true;
        rb.velocity = direction.normalized * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        fsm.SetGameState(PlayerState.Idle);

        rb.velocity = Vector2.zero;

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    public bool IsDashing => isDashing;

    public bool TryDash()
    {
        Vector2 moveDir = inp.moveDirection;
        if (isDashing || !canDash || moveDir.Equals(Vector2.zero))
        {
            return false;
        }

        print("Dashing");
        StartCoroutine(DashRoutine(moveDir));
        return true;
    }
}
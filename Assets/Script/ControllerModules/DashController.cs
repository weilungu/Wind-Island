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


    Rigidbody2D rb;
    MoveController move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<MoveController>();
    }

    IEnumerator DashRoutine(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        rb.velocity = direction.normalized * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
    
    public void TryDash()
    {
        Vector2 moveDir = move.GetMoveInput();
        if (isDashing || !canDash || moveDir.Equals(Vector2.zero))
        {
            return;
        }
    
        print("Dashing");
        StartCoroutine(DashRoutine(moveDir));
    }
}
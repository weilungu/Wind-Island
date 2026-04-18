using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing = false;
    
    // Instance
    Rigidbody2D rb;
    [SerializeField] StateMachine fsm;
    [SerializeField] DashData data;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator DashRoutine(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDir = direction.normalized;
        float elapsed = 0f;

        while (elapsed < data.dashDuration)
        {
            rb.MovePosition(rb.position + dashDir * data.dashSpeed * Time.fixedDeltaTime);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;

        if (!direction.Equals(Vector2.zero))
        {
            fsm.SetGameState(GameState.Move);
        }
        else
        {
            fsm.SetGameState(GameState.Idle);
        }

        yield return new WaitForSeconds(data.dashCooldown);

        canDash = true;
    }
    
    public bool TryDash(Vector2 direction)
    {
        if (isDashing || !canDash || direction.Equals(Vector2.zero))
        {
            return false;
        }

        StartCoroutine(DashRoutine(direction));
        fsm.SetGameState(GameState.Dash);
        return true;
    }
}
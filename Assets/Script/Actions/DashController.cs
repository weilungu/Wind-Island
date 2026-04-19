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
    Movement move;
    float castDistance => move.castDistance;
    
    [SerializeField] StateMachine fsm;
    [SerializeField] DashData data;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<Movement>();
    }
    
    IEnumerator DashRoutine(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDir = direction.normalized;
        float elapsed = 0f;

        while (elapsed < data.dashDuration)
        {
            Vector2 movement = dashDir * data.dashSpeed * Time.fixedDeltaTime;
            bool moved = TryDashMove(dashDir, movement);

            // 完全無法移動才中止 Dash
            if (!moved) break;

            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
        fsm.SetGameState(direction.Equals(Vector2.zero) ? GameState.Idle : GameState.Move);

        yield return new WaitForSeconds(data.dashCooldown);
        canDash = true;
    }

    bool TryDashMove(Vector2 dashDir, Vector2 movement)
    {
        int hitCount = rb.Cast(dashDir, move.filter, move.hitResults, movement.magnitude + move.castDistance);
        print($"dashDir:{dashDir} | hitCount:{hitCount} | distance:{(hitCount > 0 ? move.hitResults[0].distance : -1)}");
        
        if (hitCount == 0)
        {
            rb.MovePosition(rb.position + movement);
            return true;
        }

        // safeDistance 用 Mathf.Max 確保不為負數
        float safeDistance = move.hitResults[0].distance - move.castDistance;
        
        safeDistance = Mathf.Max(safeDistance, 0.01f);

        if (safeDistance > 0)
        {
            rb.MovePosition(rb.position + dashDir * safeDistance);
        }

        // 嘗試滑動
        Vector2 moveX = new Vector2(movement.x, 0);
        Vector2 moveY = new Vector2(0, movement.y);
        bool slidX = false, slidY = false;

        
        if (moveX.magnitude > 0)
        {
            int hitX = rb.Cast(moveX.normalized, move.filter, move.hitResults, Mathf.Abs(movement.x) + move.castDistance);
            if (hitX == 0) { rb.MovePosition(rb.position + moveX); slidX = true; }
        }

        if (moveY.magnitude > 0)
        {
            int hitY = rb.Cast(moveY.normalized, move.filter, move.hitResults, Mathf.Abs(movement.y) + move.castDistance);
            if (hitY == 0) { rb.MovePosition(rb.position + moveY); slidY = true; }
        }

        return slidX || slidY;
    }
    
    public bool TryDash(Vector2 direction)
    {
        if (isDashing || !canDash || direction.Equals(Vector2.zero)) return false;

        if (!CanDashInDirection(direction)) return false;

        StartCoroutine(DashRoutine(direction));
        fsm.SetGameState(GameState.Dash);
        return true;
    }

    bool CanDashInDirection(Vector2 direction)
    {
        RaycastHit2D[] check = new RaycastHit2D[1];
        
        int hitCount = rb.Cast(direction.normalized, move.filter, check, move.castDistance * 3f);
        return hitCount == 0;
    }
}
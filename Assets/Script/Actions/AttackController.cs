using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] int currCombo;
    float nextAttackTime;
    float lastAttackTime;
    float comboCooldownEndTime;
    
    Vector2 lastDirection = Vector2.right;
    
    [Header("Field Instance")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask targetLayers;
    
    [Header("Attack Data")]
    [SerializeField] AttackData data;
    
    Collider2D[] hitResults = new Collider2D[32];

    
    public bool canAttack => Time.time >= nextAttackTime && Time.time >= comboCooldownEndTime;

    Vector2 GetAttackOrigin(Vector2 attackDir)
    {
        Vector2 originBase = attackPoint != null
            ? (Vector2)attackPoint.position
            : (Vector2)transform.position;

        return originBase + attackDir * data.attackRange;
    }

    public void UpdateAttackDirection(Vector2 direction)
    {
        Vector2 normalized = direction.normalized;
        if (normalized != Vector2.zero)
        {
            lastDirection = normalized;
        }
    }

    public bool TryGetPlayerInFront(out Transform player)
    {
        player = null;

        Vector2 attackOrigin = GetAttackOrigin(lastDirection);

        int hitCount = Physics2D.OverlapBoxNonAlloc(
            attackOrigin,
            data.hitboxSize,
            0,
            hitResults,
            targetLayers);

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D hit = hitResults[i];
            if (hit == null) continue;

            if (hit.CompareTag("Player") || hit.GetComponent<PlayerController>() != null)
            {
                player = hit.transform;
                return true;
            }
        }

        return false;
    }

    void Attack(Vector2 direction)
    {
        Vector2 attackDir = direction.normalized;
        
        if (attackDir != Vector2.zero) lastDirection = attackDir;
        else attackDir = lastDirection;

        Vector2 attackOrigin = GetAttackOrigin(attackDir);
        
        // Detect Enemy in range
        int hitCount = Physics2D.OverlapBoxNonAlloc(
            attackOrigin,
            data.hitboxSize,
            0,
            hitResults,
            targetLayers);
        
        // Damage
        for (int i = 0; i < hitCount; i++)
        {
            print($"Hit {hitResults[i].name}");
            hitResults[i].GetComponent<Health>().TakeDamage(data.damage);
        }
    }
    public void TryAttack(Vector2 direction)
    {
        if (!canAttack) return;
        
        if (currCombo > 0 && Time.time - lastAttackTime > data.comboResetTime)
        {
            currCombo = 0;
        }

        currCombo++;
        lastAttackTime = Time.time;
        nextAttackTime = Time.time + data.attackRate;

        Attack(direction);

        if (currCombo >= data.maxCombo)
        {
            currCombo = 0;
            comboCooldownEndTime = Time.time + data.comboCooldown;
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        
        Vector2 attackOrigin = (Vector2)attackPoint.position + lastDirection * data.attackRange;
        Gizmos.DrawWireCube(attackOrigin, 
                        new Vector3(data.hitboxSize.x, data.hitboxSize.y, 1));
    }
}

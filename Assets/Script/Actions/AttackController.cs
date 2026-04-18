using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    float nextAttackTime;
    [SerializeField] int currCombo;
    float lastAttackTime;
    float comboCooldownEndTime;
    
    Vector2 lastAttackDirection = Vector2.right;
    
    [Header("Field Instance")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask targetLayers;
    
    [Header("Attack Data")]
    [SerializeField] AttackData data;
    
    Collider2D[] hitResults = new Collider2D[32];

    
    public bool canAttack => Time.time >= nextAttackTime && Time.time >= comboCooldownEndTime;
    public void UpdateAttackDirection(Vector2 direction)
    {
        Vector2 normalized = direction.normalized;
        if (normalized != Vector2.zero)
        {
            lastAttackDirection = normalized;
        }
    }

    void Attack(Vector2 direction)
    {
        Vector2 attackDir = direction.normalized;
        if (attackDir != Vector2.zero)
        {
            lastAttackDirection = attackDir;
        }
        else
        {
            attackDir = lastAttackDirection;
        }

        Vector2 originBase = !attackPoint.Equals(null) ? (Vector2)attackPoint.position : (Vector2)transform.position;
        Vector2 attackOrigin = originBase + attackDir * data.attackRange;

        
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
        
        Vector2 attackOrigin = (Vector2)attackPoint.position + lastAttackDirection * data.attackRange;
        Gizmos.DrawWireCube(attackOrigin, 
                        new Vector3(data.hitboxSize.x, data.hitboxSize.y, 1));
    }
}

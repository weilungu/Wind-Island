using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    float nextAttackTime;
    Vector2 lastAttackDirection = Vector2.right;
    
    [Header("Field Instance")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask targetLayers;
    
    [Header("Attack Values")]
    [SerializeField] float attackRate = 1f;
    [SerializeField] float attackRange;
    
    Collider2D[] hitResults = new Collider2D[32];

    public bool canAttack => Time.time >= nextAttackTime;

    public void UpdateAttackDirection(Vector2 direction)
    {
        Vector2 normalized = direction.normalized;
        if (normalized != Vector2.zero)
        {
            lastAttackDirection = normalized;
        }
    }

    void Attack( Vector2 direction, int damage)
    {
        Vector2 attackDir = direction.normalized;
        if (attackDir != Vector2.zero)
        {
            lastAttackDirection = attackDir;
        }

        // Keep attacking toward the previous movement direction while idle.
        if (attackDir == Vector2.zero)
        {
            attackDir = lastAttackDirection;
        }

        Vector2 originBase = attackPoint != null ? (Vector2)attackPoint.position : (Vector2)transform.position;
        Vector2 attackOrigin = originBase + attackDir * attackRange;

        
        // Detect Enemy in range
        int hitCount = Physics2D.OverlapBoxNonAlloc(
            attackOrigin,
            new Vector2(attackRange, 0),
            0,
            hitResults,
            targetLayers);
        
        // Damage
        for (int i = 0; i < hitCount; i++)
        {
            print($"Hit {hitResults[i].name}");
            hitResults[i].GetComponent<Health>().TakeDamage(damage);
        }
    }
    public void TryAttack(Vector2 direction, int damage)
    {
        if (!canAttack) return;

        float cooldown = attackRate > 0f ? attackRate : 1f;
        nextAttackTime = Time.time + cooldown;

        Attack(direction, damage);
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        
        Vector2 attackOrigin = (Vector2)attackPoint.position + lastAttackDirection * attackRange;
        Gizmos.DrawWireSphere(attackOrigin, attackRange);
        // Gizmos.DrawWireCube(/*attackPoint.position*/ attackOrigin, new Vector3(attackRange, attackRangeY, 1) * lastAttackDirection);
    }
}

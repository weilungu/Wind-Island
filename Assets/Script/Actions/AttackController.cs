using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] float attackRate = 1f;
    float nextAttackTime;
    
    [Header("Field Instance")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask targetLayers;
    
    [Header("Attack Values")]
    [SerializeField] float attackRangeX;
    [SerializeField] float attackRangeY;
    
    Collider2D[] hitResults = new Collider2D[32];

    public bool canAttack => Time.time >= nextAttackTime;

    void Attack( Vector2 direction, int damage)
    {
        Vector2 attackDir = direction.normalized;
    
        Vector2 attackOrigin = (Vector2)transform.position + attackDir * attackRangeX;

        
        // Detect Enemy in range
        int hitCount = Physics2D.OverlapBoxNonAlloc(
            // attackPoint.position,
            attackOrigin,
            new Vector2(attackRangeX, 0),
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
        if (attackPoint.Equals( null)) return;
        
        // Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}

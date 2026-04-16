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
    [SerializeField] float attackRange;
    
    Collider2D[] hitResults = new Collider2D[32];

    public bool canAttack => Time.time >= nextAttackTime;

    void Attack(int damage)
    {
        // Detect Enemy in range
        int hitCount = Physics2D.OverlapCircleNonAlloc(
            attackPoint.position,
            attackRange,
            hitResults,
            targetLayers);
        
        // Damage
        for (int i = 0; i < hitCount; i++)
        {
            print($"Hit {hitResults[i].name}");
            hitResults[i].GetComponent<Health>().TakeDamage(damage);
        }
    }
    public void TryAttack(int damage)
    {
        if (!canAttack) return;

        float cooldown = attackRate > 0f ? attackRate : 1f;
        nextAttackTime = Time.time + cooldown;

        Attack(damage);
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint.Equals( null)) return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

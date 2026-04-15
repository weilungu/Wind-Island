using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    
    [Header("Field Instance")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayers;
    
    [Header("Attack Values")]
    [SerializeField] float attackRange;
    
    Collider2D[] hitResults = new Collider2D[32];

    public void Attack()
    {
        // Detect Enemy in range
        int hitCount = Physics2D.OverlapCircleNonAlloc(
            attackPoint.position,
            attackRange,
            hitResults,
            enemyLayers);
        
        // Damage
        for (int i = 0; i < hitCount; i++)
        {
            print($"Hit {hitResults[i].name}");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint.Equals(null)) return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

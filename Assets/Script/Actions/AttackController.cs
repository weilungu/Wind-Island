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
    [SerializeField] AttackData atkData;
    
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
        Vector2 attackOrigin = originBase + attackDir * atkData.attackRange;

        
        // Detect Enemy in range
        int hitCount = Physics2D.OverlapBoxNonAlloc(
            attackOrigin,
            new Vector2(atkData.attackRange, 0),
            0,
            hitResults,
            targetLayers);
        
        // Damage
        for (int i = 0; i < hitCount; i++)
        {
            print($"Hit {hitResults[i].name}");
            hitResults[i].GetComponent<Health>().TakeDamage(atkData.damage);
        }
    }
    public void TryAttack(Vector2 direction)
    {
        if (!canAttack) return;
        
        if (currCombo > 0 && Time.time - lastAttackTime > atkData.comboResetTime)
        {
            currCombo = 0;
        }

        currCombo++;
        lastAttackTime = Time.time;
        nextAttackTime = Time.time + atkData.attackRate;

        Attack(direction);

        if (currCombo >= atkData.maxCombo)
        {
            currCombo = 0;
            comboCooldownEndTime = Time.time + atkData.comboCooldown;
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        
        Vector2 attackOrigin = (Vector2)attackPoint.position + lastAttackDirection * atkData.attackRange;
        Gizmos.DrawWireSphere(attackOrigin, atkData.attackRange);
    }
}

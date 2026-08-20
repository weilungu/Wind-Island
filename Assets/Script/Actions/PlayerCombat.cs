using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] int maxAttackSteps = 3;
    
    [Header("Attack Detection")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayers;
    
    [Header("Timing Settings")]
    [SerializeField, Range(0f, 10f), Tooltip("攻擊達最高次數, 冷卻數秒並重置目前次數")]
    float cooldown = 1f;
    
    [SerializeField, Range(0f, 10f), Tooltip("未達最高次數 && 超過時間, 重置目前次數")]
    float timeout = 1f;
    
    
    [Header("Debug Show")]
    [SerializeField] int currAttackStep = 0;
    
    [Space]
    [SerializeField] Color rangeColor = Color.red;

    
    // Private Values
    Coroutine timeoutCoroutine;
    Coroutine cooldownCoroutine;
    
    Collider2D[] hitEnemies = new Collider2D[1];
    
    
    // === Life Cycle ===
    void OnDrawGizmosSelected()
    {
        if (attackPoint is null) return;
        
        Gizmos.color = rangeColor;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // === Self Method === //
    void ResetCurrentStep() => currAttackStep = 0;

    IEnumerator CountDownCoroutine(float timesType)
    {
        yield return new WaitForSeconds(timesType);
        
        ResetCurrentStep();
    }

    void ResetCoroutines(ref Coroutine target, float timesType)
    {
        if (timeoutCoroutine is not null)
            StopCoroutine(timeoutCoroutine);
        
        if (cooldownCoroutine is not null)
            StopCoroutine(cooldownCoroutine);
        
        
        if (target is not null)
            StopCoroutine(target);
        
        target = StartCoroutine(CountDownCoroutine(timesType));
    }

    
    // === Self API === //
    public void Attack()
    {
        int enemiesNum = Physics2D.OverlapCircleNonAlloc(
            point: attackPoint.position,
            radius: attackRange,
            results: hitEnemies,
            layerMask: enemyLayers);

        if (enemiesNum > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                print($"We hit {enemy.name}");
            }
        }
    }

    public void AdvanceStep()
    {
        currAttackStep++;

        if (!CanAttack)
        {
            ResetCoroutines(ref cooldownCoroutine, cooldown);
            return;
        }
        ResetCoroutines(ref timeoutCoroutine, timeout);
    }
    
    
    public bool CanAttack => currAttackStep < maxAttackSteps;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] int maxAttackSteps = 3;
    [SerializeField, Range(0f, 10f)] float attackTimeout = 1f;
    
    [Header("Cooldown")]
    [SerializeField, Range(0f, 10f)] float attackCooldown = 1f;
    
    [Header("Debug Show")]
    [SerializeField] int currAttackStep = 0;

    // Private Values
    Coroutine timeoutCoroutine;
    Coroutine cooldownCoroutine;
    
    
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
        print("Attacked");
    }

    public void AdvanceStep()
    {
        currAttackStep++;

        if (!CanAttack)
        {
            ResetCoroutines(ref cooldownCoroutine, attackCooldown);
            return;
        }
        ResetCoroutines(ref timeoutCoroutine, attackTimeout);
    }
    
    
    public bool CanAttack => currAttackStep < maxAttackSteps;
}

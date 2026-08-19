using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Count")]
    [SerializeField] int maxAttackSteps = 3;
    [SerializeField, Range(0f, 1f)] float attackResetTime = 0.5f;
    
    [Header("Cooldown")]
    [SerializeField, Range(0f, 10f)] float attackCooldown = 0.5f;
    
    [Header("Debug Show")]
    [SerializeField] int currAttackStep = 0;
    
    
    // === Self Method === //
    void ResetCurrentStep() => currAttackStep = 0;

    IEnumerator AfterDelayCoroutine(float timesType)
    {
        yield return new WaitForSeconds(timesType);
        
        ResetCurrentStep();
    }
    

    // === Self API === //
    public void AdvanceStep() => currAttackStep++;
    
    public void Attack()
    {
        print("Attacked");
    }
    
    public void Reset_AfterDelay(float timesType)
        => StartCoroutine(AfterDelayCoroutine(timesType));
    

    public bool CanAttack => currAttackStep < maxAttackSteps;
    public float AttackResetTime => attackResetTime;
    public float AttackCooldown => attackCooldown;
}

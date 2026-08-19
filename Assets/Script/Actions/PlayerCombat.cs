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
    [SerializeField, Range(0f, 1f)] float attackCooldown = 0.5f;
    [SerializeField, Range(0f, 1f)] float comboCooldown = 0.5f;
    
    [Header("Debug Show")]
    [SerializeField] int currAttackStep = 0;
    
    
    // === Self API === //
    public void Attack()
    {
        print("Attacked");
    }

    public bool CanContiAttack => currAttackStep < maxAttackSteps;
}

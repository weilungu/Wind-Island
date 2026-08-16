using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerInput input;
    Animator anim;
    
    
    // === Unity Life Cycle === //
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        
        anim = GetComponentInChildren<Animator>();
    }
    
    
    // === Self API === //
    public void Attack()
    {
        // anim.Play("Attack");
        print("Attacked");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // === Awake Values === //
    Animator anim;
    
    
    [Header("Values")]
    [SerializeField] string animationName = "Attack";
    
    // === Private Values === //
    int animHash;
    
    
    // === Unity Life Cycle === //
    void OnEnable()
    {
        animHash = Animator.StringToHash(animationName);
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    
    
    // === Self API === //
    public void Attack()
    {
        anim.Play(animHash);
        
        print("Attacked");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth;
    
    [Header("Debug Show")]
    [SerializeField] int currHealth;

    
    // === Unity Life Cycle === //
    void Start()
    {
        currHealth = maxHealth;
    }
    
    
    // === Self Method === //
    void Dead()
    {
        gameObject.SetActive(false);
        
        print($"{name} was Dead");
    }
    
    // === Self API === //
    public void TakeDamage(int amount)
    {
        currHealth -= amount;
        
        if (currHealth <= 0)
            Dead();
    }
    
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    public int currentHealth { get; private set; }
    
    // Delegate
    public event Action<int> OnHealthChanged;
    public event Action<int> OnMaxHealthChanged;
    public event Action OnDeath;
    
    void Start()
    {
        currentHealth = maxHealth;
        OnMaxHealthChanged?.Invoke(maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthChanged?.Invoke(currentHealth);
        print("OnHealthChanged");
    
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}

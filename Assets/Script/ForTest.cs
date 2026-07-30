using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTest : MonoBehaviour
{

    Health targetHealth;
    
    [Header("Target")]
    [SerializeField] int damaged = 20;

    void Awake()
    {
        targetHealth = GetComponent<Health>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            targetHealth.TakeDamage(damaged);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosture : MonoBehaviour
{
    [Header("Debug Show")]
    [SerializeField] int currPosture = 0;
    
    [Header("Values")]
    [SerializeField] int maxPosture = 100;
    
    
    // Observer Pattern Part
    public event Action<int, int> OnPostureChanged;
    public event Action OnPostureReset;
    
    
    // === Unity LifeCycle === //
    void Start()
    {
        OnPostureReset?.Invoke();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPostureChanged?.Invoke(maxPosture, currPosture);
            print("clicked");
        }
    }
}

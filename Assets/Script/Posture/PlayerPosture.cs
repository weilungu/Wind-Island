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
    public event Action OnPostureChanged;
    public event Action OnPostureReset;
    
    
    // === Unity LifeCycle === //
    void Start()
    {
        OnPostureReset?.Invoke();
    }
    
    
    // === Self API === //
    public void TakePosture(int amount)
    {
        currPosture += amount;
        OnPostureChanged?.Invoke();
    }
    
    public void ResetPosture() => OnPostureReset?.Invoke();
    
    public int CurrPosture => currPosture;
    public int MaxPosture => maxPosture;
}

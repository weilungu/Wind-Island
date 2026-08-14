using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosture : MonoBehaviour
{
    [Header("Debug Show")]
    [SerializeField] float currPosture = 0f;
    
    [Header("Values")]
    [SerializeField] float maxPosture = 100f;
    [SerializeField, Range(0f, 50f)] float timeGap = 10f;
    [SerializeField] float decreaseRate;  // 每秒下降多少 
    
    // Observer Pattern Part
    public event Action OnPostureChanged;
    public event Action OnPostureReset;
    
    
    // === Unity LifeCycle === //
    void Start()
    {
        OnPostureReset?.Invoke();
    }

    void Update()
    {
        Invoke("DecreasePosture", timeGap);
    }

    // === Self API === //
    public void TakePosture(float amount)
    {
        currPosture += amount;
        OnPostureChanged?.Invoke();
    }

    public void DecreasePosture()
    {
        if (currPosture <= 0) return;
        
        currPosture -= decreaseRate * Time.deltaTime;
        OnPostureChanged?.Invoke();
    }
    
    public void ResetPosture() => OnPostureReset?.Invoke();
    
    public float CurrPosture => currPosture;
    public float MaxPosture => maxPosture;
}

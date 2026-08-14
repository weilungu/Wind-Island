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
    public event Action OnChanged;
    public event Action OnReset;
    
    
    // === Unity LifeCycle === //
    void Start()
    {
        OnReset?.Invoke();
    }

    void Update()
    {
        Invoke("DecreasePosture", timeGap);
    }

    // === Self API === //
    public void TakePosture(float amount)
    {
        currPosture += amount;
        OnChanged?.Invoke();
    }

    public void DecreasePosture()
    {
        if (currPosture <= 0) return;
        
        currPosture -= decreaseRate * Time.deltaTime;
        OnChanged?.Invoke();
    }
    
    public void ResetPosture() => OnReset?.Invoke();
    
    public float CurrPosture => currPosture;
    public float MaxPosture => maxPosture;
}

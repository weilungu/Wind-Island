using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosture : MonoBehaviour
{
    [Header("Debug Show")]
    [SerializeField] float currPosture = 0f;
    
    [Header("Set Posture")]
    [SerializeField] float maxPosture = 100f;
    
    [Header("Set Decrease")]
    [SerializeField] float decreaseRate = 10f;  // 每秒下降多少 
    [SerializeField, Range(0f, 10f)] float timeGap = 10f;
    
    // Private Values
    float tempTimeGap;
    
    // Observer Pattern Part
    public event Action OnChanged;
    public event Action OnReset;
    
    
    // === Unity LifeCycle === //
    void Start()
    {
        tempTimeGap = timeGap;
        
        OnReset?.Invoke();
    }

    void Update()
    {
        WaitToDecrease();
    }

    // === Self Method === //
    float CountDown(ref float time)
    {
        if (time <= 0)
        {
            time = 0f;
            return 0f;
        }
        
        time -= Time.deltaTime;
        return time;
    }
    void DecreasePosture()
    {
        if (currPosture <= 0) return;
        
        currPosture -= decreaseRate * Time.deltaTime;
        OnChanged?.Invoke();
    }

    void WaitToDecrease()
    {
        float currTime = CountDown(ref timeGap);
        // print(currTime);
        
        if (currTime <= 0f)
            DecreasePosture();
    }
    

    // === Self API === //
    public void TakePosture(float amount)
    {
        currPosture += amount;
        OnChanged?.Invoke();
    }

    public void ResetPosture() => OnReset?.Invoke();
    public void ResetTimeGap() => timeGap = tempTimeGap;
    
    public float CurrPosture => currPosture;
    public float MaxPosture => maxPosture;
}

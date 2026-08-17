using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosture : MonoBehaviour
{
    
    [Header("Posture Values")]
    [SerializeField] float maxPosture = 100f;
    
    [Space]
    [SerializeField, Tooltip("每秒衰減多少")]
    float decreaseRate = 10f; 
    
    [SerializeField, Range(0f, 10f), Tooltip("隔多少時間後衰減")]
    float timeGap = 1f;
    
    
    [Header("Debug Show")]
    [SerializeField, Tooltip("目前的 Posture (別動)")]
    float currPosture = 0f;
    
    // Private Values
    float tempTimeGap;
    
    // Observer Pattern Part
    public event Action OnChanged;
    public event Action OnReset;
    
    
    // === Unity LifeCycle === //
    void Start()
    {
        tempTimeGap = timeGap;
        timeGap = 0f;
        
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
        if (currPosture <= 0f)
        {
            currPosture = 0f;
            return;
        }
        
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

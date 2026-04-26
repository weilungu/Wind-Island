using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Posture : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private float currValue = 0f;
    
    [Header("Values")]
    [SerializeField] private float maxValue = 100f;
    
    [SerializeField] private float growthVelocity = 1f;
    [SerializeField] private float growthRate = 0.01f;
    
    public event Action OnPostureReset;
    public event Action<float, float> OnPostureChanged;

    public IEnumerator TakePostureDamage(float value)
    {
        if (currValue >= maxValue) yield return null;
        
        float targetValue = currValue + value;
        while (currValue < targetValue)
        {
            if (growthVelocity >= targetValue)
            {
                currValue = targetValue;
                
                OnPostureChanged?.Invoke(maxValue, currValue);
                yield break;
            }
            
            currValue += growthVelocity;
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }
    }
    public IEnumerator Recovery(float value)
    {
        if (currValue <= 0) yield return null;
        
        float targetValue = currValue - value;
        while (currValue >= targetValue)
        {
            currValue -= growthVelocity;
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }
    }
    public void ResetWithTime(float seconds)
    {
        
    }
    
    void PostureBroken()
    {
        OnPostureReset?.Invoke();
    }
}

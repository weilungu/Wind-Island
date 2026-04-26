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
        float targetValue = currValue + value;
        if (growthVelocity >= targetValue)
        {
            currValue = targetValue;
            yield break;
        }
        
        while (currValue < targetValue)
        {
            if (currValue >= maxValue)
            {
                currValue = maxValue;
                yield break;
            }
            
            currValue += growthVelocity;
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }
    }
    public IEnumerator Recovery(float value)
    {
        float targetValue = currValue - value;
        if (growthVelocity >= targetValue)
        {
            currValue = targetValue;
            yield break;
        }
        
        while (currValue >= targetValue)
        {
            if (currValue <= 0)
            {
                print("haha");
                currValue = 0;
                yield break;
            }
            
            currValue -= growthVelocity;
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }
    }
    public IEnumerator SlowReset(float seconds)
    {
        while (currValue >= 0)
        {
            currValue -= growthVelocity;
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }
        
        yield break;
    }
    
    void PostureBroken()
    {
        OnPostureReset?.Invoke();
    }
}

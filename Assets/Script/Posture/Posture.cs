using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posture : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float maxValue = 100f;
    [SerializeField] float currValue = 0f;
    
    public event Action OnPostureReset;
    public event Action<float, float> OnPostureChanged;

    public void TakePosture(float value)
    {
        if (currValue >= maxValue) return;
        
        currValue += value;
        print(currValue);
        OnPostureChanged?.Invoke(maxValue, currValue);
    }

    void PostureBroken()
    {
        OnPostureReset?.Invoke();
    }
}

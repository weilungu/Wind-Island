using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Posture : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float maxValue = 100f;
    [SerializeField] private float currValue = 0f;
    [SerializeField] public float recoveryTime = 2f;
    
    public event Action OnPostureReset;
    public event Action<float, float> OnPostureChanged;

    public void TakePostureDamage(float value)
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

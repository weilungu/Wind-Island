using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image[] fills;
    
    [SerializeField] Image fillLeft;
    [SerializeField] Image fillRight;
    
    [Header("Target")]
    [SerializeField] PlayerPosture posture;

    
    // === Unity Life Cycle === //
    void OnEnable()
    {
        posture.OnPostureChanged += PostureUpdate;
        posture.OnPostureReset += ResetPosture;
    }
    
    void OnDisable()
    {
        posture.OnPostureChanged -= PostureUpdate;
        posture.OnPostureReset -= ResetPosture;
    }


    // === Self Method === //
    void PostureUpdate(int maxPosture, int currPosture)
    {
        float fill = (float)currPosture / (float)maxPosture;
        
        SetFills(fill);
    }
    void ResetPosture()
    {
        float fill = 0f;
        
        SetFills(fill);
    }
    
    void SetFills(float fill)
    {
        foreach (Image f in fills)
            f.fillAmount = fill;
    }
}
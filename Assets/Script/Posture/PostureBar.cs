using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image fillLeft;
    [SerializeField] private Image fillRight;
    
    [Header("Target")]
    [SerializeField] private Posture posture;

    void Start()
    {
        posture.OnPostureChanged += PostureUpdate;
        posture.OnPostureReset += ResetPosture;
        
        ResetPosture();
    }


    void PostureUpdate(float maxPosture, float currPosture)
    {
        float fill = currPosture / maxPosture;

        fillRight.fillAmount = fill;
        fillLeft.fillAmount = fill;
    }
    void ResetPosture()
    {
        fillRight.fillAmount = 0;
        fillLeft.fillAmount = 0;
        print("reset posture");
    }

    void RecoveryPosture()
    {
        if (fillRight.fillAmount == 0 && fillLeft.fillAmount == 0) return;

        // while (posture.recoveryTime)
        // {
        //     
        // }
    }
}
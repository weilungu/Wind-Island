using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image fillLeft;
    [SerializeField] Image fillRight;
    
    [Header("Target")]
    [SerializeField] Posture posture;

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
        print(fill);
    }
    void ResetPosture()
    {
        fillRight.fillAmount = 0;
        fillLeft.fillAmount = 0;
        print("reset posture");
    }
}
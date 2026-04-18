using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    [Header("Images")] [SerializeField] Image fillLeft;
    [SerializeField] Image fillRight;

    [Header("Values")]
    // [SerializeField] float mixValue = 0;
    [SerializeField] float maxValue = 100;
    [SerializeField] float values = 0;
    

    public void AddPosture(float posture)
    {
        values += posture;
        float fill = values / maxValue;

        fillRight.fillAmount = fill;
        fillLeft.fillAmount = fill;
        print(fill);
    }

    void ResetPosture()
    {
        values = 0;
    }
    
    
}
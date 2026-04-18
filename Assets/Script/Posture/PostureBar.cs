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
    [SerializeField] float maxValue = 100;
    [SerializeField] float values = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AddPosture(10);
        }

        if (Input.GetMouseButtonDown(2))
        {
            ResetPosture();
        }
    }

    public void UpdatePosture()
    {
        float fill = values / maxValue;

        fillRight.fillAmount = fill;
        fillLeft.fillAmount = fill;
        print(fill);
    }
    public void AddPosture(float posture)
    {
        if (values >= maxValue) return; 
        
        values += posture;
        UpdatePosture();
    }
    public void ResetPosture()
    {
        values = 0;
        UpdatePosture();
    }
    
    
}
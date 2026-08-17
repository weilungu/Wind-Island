using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardBreak : MonoBehaviour
{
    [Header("Values")]
    [SerializeField, Range(0f, 100f)] float speedPercent = 50f;
    [SerializeField, Range(0f, 100f)] float exitThresholdPercent = 60f;
    
    [Header("Debug Show")]
    [SerializeField] bool isGuardBreak;
    
    // Awake Values
    PlayerPosture posture;

    
    // === Unity Life Cycle === //
    void Awake()
    {
        posture = GetComponent<PlayerPosture>();
    }

    
    // === Self API === //
    public void SetGuardBreakBe(bool enable) => isGuardBreak = enable;
    
    public float SpeedPCT => speedPercent;
    public bool Less_ThreshPCT => 
        posture.CurrPosture < posture.MaxPosture * (exitThresholdPercent / 100);
    
    public bool IsGuardBreak => isGuardBreak;
}

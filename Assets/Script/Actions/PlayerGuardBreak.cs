using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardBreak : MonoBehaviour
{
    [Header("Values")]
    [SerializeField, Range(0f, 100f)] float speedPercent = 100f;
    
    [Header("Debug Show")]
    [SerializeField] bool isGuardBreak;

    // === Self API === //
    public void SetActive(bool enable) => isGuardBreak = enable;
    
    public float SpeedPercent => speedPercent;
    public bool IsGuardBreak => isGuardBreak;
}

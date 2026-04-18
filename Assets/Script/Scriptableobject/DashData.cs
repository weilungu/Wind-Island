using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashData", menuName = "Dash/DashData")]
public class DashData : ScriptableObject
{
    [Header("Be Dashed")]
    public bool canDash = true;
    public bool isDashing = false;
    
    [Header("Dash Values")]
    public float dashSpeed = 12;
    public float dashDuration = 0.3f;
    public float dashCooldown = 0.5f;

    [Header("Posture")]
    public float posture = 10;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Attack/AttackData")]
public class AttackData : ScriptableObject
{
    [Header("Attack Values")]
    public float attackRate;
    public float attackRange;
    public int damage;
    
    [Header("Combo")]
    public int maxCombo = 3;
    public float comboCooldown = 1f;
    public float comboResetTime = 0.5f;
    
    public Vector2 hitboxSize; // 之後換 OverlapBox 用得到
}

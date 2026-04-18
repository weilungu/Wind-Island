using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Attack/AttackData")]
public class AttackData : ScriptableObject
{
    [Header("Attack Values")] public float attackRate = 0.3f;
    public float attackRange = 0.5f;
    public int damage = 10;

    [Header("Combo")] public int maxCombo = 3;
    public float comboCooldown = 1f;
    public float comboResetTime = 0.5f;

    [Header("Posture")]
    public float posture = 10;

    [Header("Other")] public Vector2 hitboxSize; // about OverlapBox
}
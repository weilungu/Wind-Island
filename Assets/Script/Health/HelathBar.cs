using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelathBar : MonoBehaviour // HealthBar 放在 UI
{
    private Slider slider;

    [SerializeField] private Health target; // 使用 Player, Enemy 上的 Health

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        if (target is null) return;

        // Subscribe => 若被觸發，直接呼叫 Method
        target.OnMaxHealthChanged += SetMaxHealth;
        target.OnHealthChanged += SetHealth;
    }


    void SetHealth(int health)
    {
        slider.value = health;
    }

    void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        SetHealth(health);
    }
}
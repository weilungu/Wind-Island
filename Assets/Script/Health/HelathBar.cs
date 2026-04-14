using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelathBar : MonoBehaviour // HealthBar 放在 UI
{
    Slider slider;

    [SerializeField] Health target; // 使用 Player, Enemy 上的 Health

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        if (target.Equals(null)) return;

        // Subscribe => 若被觸發，直接呼叫 Method
        target.OnMaxHealthChanged += SetMax;
        target.OnHealthChanged += SetHealth;
    }


    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMax(int health)
    {
        slider.maxValue = health;
        SetHealth(health);
    }
}
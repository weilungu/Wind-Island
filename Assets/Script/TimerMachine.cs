using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMachine : MonoBehaviour
{
    public float timer = 0;
    public bool allowRun = false;

    void Start()
    {
        InitializeTimer();
    }

    void Update()
    {
        if (allowRun)
        {
            timer += Time.deltaTime;
        }
    }

    void InitializeTimer()
    {
        timer = 0;
        allowRun = false;
    }

    public void Run()
    {
        allowRun = true;
    }
    public void Pause()
    {
        allowRun = false;
    }
    public void ResetTimer()
    {
        timer = 0;
    }
}

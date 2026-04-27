using UnityEngine;

public class TimerMachine
{
    public float timer = 0f;
    private bool allowRun = false;

    public void InitializeTimer()
    {
        timer = 0f;
        allowRun = false;
    }

    public void Tick(float deltaTime)
    {
        if (!allowRun)
        {
            return;
        }

        timer += deltaTime;
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
        timer = 0f;
    }
}

using System;
using System.Collections;
using UnityEngine;

public class Posture : MonoBehaviour
{
    // Properties
    [Header("Debug")]
    [SerializeField] private float currValue = 0f;
    [SerializeField] private bool postureIncreased = false;
    
    [Header("Values")]
    [SerializeField] private float maxValue = 100f;
    
    [SerializeField] private float growthVelocity = 1f;
    [SerializeField] private float growthRate = 0.01f;
    [SerializeField] private float slowResetRate = 0.01f;
    
    [SerializeField] private float resetTime = 0.5f;
    
    private TimerMachine timer;
    private bool isSlowResetting = false;
    private Coroutine slowResetRoutine;
    
    public event Action OnPostureReset;
    public event Action<float, float> OnPostureChanged;

    
    // Methods
    private void Start()
    {
        timer = new TimerMachine();
        timer.InitializeTimer();
    }
    private void Update()
    {
        DelayedReset();
    }

    
    void DelayedReset()
    {
        if (!postureIncreased || isSlowResetting) return;

        timer.Tick();
        if (timer.timer < resetTime) return;

        postureIncreased = false;
        timer.Pause();
        timer.ResetTimer();
        
        slowResetRoutine ??= StartCoroutine(SlowlyReset());
    }
    
    public IEnumerator TakePostureDamage(float value)
    {
        if (value <= 0f) yield break;

        if (isSlowResetting && slowResetRoutine is not null)
        {
            StopCoroutine(slowResetRoutine);
            slowResetRoutine = null;
            isSlowResetting = false;
        }

        float targetValue = Mathf.Min(currValue + value, maxValue);
        if (targetValue <= currValue) yield break;
        
        
        while (currValue < targetValue)
        {
            currValue = Mathf.Min(currValue + growthVelocity, targetValue);
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }

        postureIncreased = currValue > 0f;
        timer.ResetTimer();
        timer.Run();
    }
    public IEnumerator Recovery(float value)
    {
        if (value <= 0f) yield break;

        float targetValue = currValue - value;
        if (targetValue >= currValue) yield break;
        
        while (currValue > targetValue)
        {
            currValue = Mathf.Max(currValue - growthVelocity, targetValue, 0f);
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }

        if (currValue <= 0f)
        {
            currValue = 0f;
            postureIncreased = false;
            timer.Pause();
            timer.ResetTimer();
            PostureBroken();
        }
    }
    public IEnumerator SlowlyReset()
    {
        isSlowResetting = true;

        while (currValue > 0)
        {
            currValue = Mathf.Max(currValue - growthVelocity, 0f);
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(slowResetRate);
        }

        currValue = 0f;
        isSlowResetting = false;
        slowResetRoutine = null;
        PostureBroken();
    }
    
    void PostureBroken()
    {
        print("Broken");
        OnPostureReset?.Invoke();
    }
}

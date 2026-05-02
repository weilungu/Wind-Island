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
    
    [Header("Growth")]
    [SerializeField] private float growthVelocity = 1f;
    [SerializeField] private float growthRate = 0.01f;
    [SerializeField] private float slowResetRate = 0.01f;
    [SerializeField] private float resetTime = 0.5f;

    [Header("Guard Break")]
    public bool isFull = false;
    public float guardSpeed = 2;
    
    private TimerMachine timer;
    private bool isSlowResetting = false;
    private Coroutine slowResetRoutine;
    private Coroutine recoveryRoutine;
    private Coroutine damageRoutine;
    
    public event Action OnPostureReset;
    public event Action<float, float> OnPostureChanged;

    
    // Methods
    void Start()
    {
        timer = new TimerMachine();
        timer.InitializeTimer();
    }
    void Update()
    {
        DelayedReset();
        // 安全限制在 0~maxValue，避免超出造成無限增加錯覺
        currValue = Mathf.Clamp(currValue, 0f, maxValue);
        isFull = currValue >= maxValue;
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

        if (currValue >= maxValue)
        {
            currValue = maxValue;
            yield break;
        }

        if (recoveryRoutine is not null)
        {
            StopCoroutine(recoveryRoutine);
            recoveryRoutine = null;
        }

        if (isSlowResetting && slowResetRoutine is not null)
        {
            StopCoroutine(slowResetRoutine);
            slowResetRoutine = null;
            isSlowResetting = false;
        }

        float targetValue = Mathf.Min(currValue + value, maxValue);
        if (targetValue <= currValue) yield break; // 判段是否繼續增加攻擊
        
        
        while (currValue < targetValue)
        {
            currValue = Mathf.Min(currValue + growthVelocity, targetValue);
            OnPostureChanged?.Invoke(maxValue, currValue);
            yield return new WaitForSeconds(growthRate);
        }

        postureIncreased = currValue > 0f;
        timer.ResetTimer();
        timer.Run();

        damageRoutine = null;
    }
    public IEnumerator Recovery(float value)
    {
        if (value <= 0f) yield break;

        if (currValue <= 0f) yield break;

        float targetValue = Mathf.Max(currValue - value, 0f);
        if (targetValue >= currValue) yield break;
        
        while (currValue > targetValue)
        {
            currValue = Mathf.Max(currValue - growthVelocity, targetValue);
            
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

        recoveryRoutine = null;
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

    // 強制重置姿態（用於立即清除滿值並避免立刻重入 GuardBreak）
    public void ForceBroken()
    {
        // 停止任何正在進行的慢速重置
        if (isSlowResetting && slowResetRoutine is not null)
        {
            StopCoroutine(slowResetRoutine);
            slowResetRoutine = null;
            isSlowResetting = false;
        }

        if (recoveryRoutine is not null)
        {
            StopCoroutine(recoveryRoutine);
            recoveryRoutine = null;
        }

        if (damageRoutine is not null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }

        currValue = 0f;
        postureIncreased = false;
        timer.Pause();
        timer.ResetTimer();
        OnPostureChanged?.Invoke(maxValue, currValue);
        PostureBroken();
    }

    public void StartDamageRoutine(float value)
    {
        if (value <= 0f) return;

        if (damageRoutine is not null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }

        damageRoutine = StartCoroutine(TakePostureDamage(value));
    }

    public void StartRecoveryRoutine(float value)
    {
        if (value <= 0f) return;

        if (recoveryRoutine is not null)
        {
            StopCoroutine(recoveryRoutine);
            recoveryRoutine = null;
        }

        recoveryRoutine = StartCoroutine(Recovery(value));
    }
}

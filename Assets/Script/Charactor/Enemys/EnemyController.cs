using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected MoveController move;
    protected EnemyAttack attack;
    protected DashController dash;
    [SerializeField] protected Posture posture;
    [SerializeField] protected Transform target;
    
    
    protected Vector2 faceDir = Vector2.zero;
    protected EnemyState enemyState;
    protected bool isInGuardBreak = false;
    protected float originalMoveSpeed = 0f;

    
    [Header("Debug")] 
    [SerializeField] protected bool hasPlayerInFront;
    [Header("GuardBreak")]
    [SerializeField] protected float guardBreakDuration = 0.5f;
    [SerializeField] protected float guardBreakMoveSpeed = 2f;

    protected virtual void Awake()
    {
        move = GetComponent<MoveController>();
        attack = GetComponent<EnemyAttack>();
        dash = GetComponent<DashController>();
        if (posture is null)
            posture = GetComponent<Posture>();
    }
    protected virtual void Start()
    {
        SetEnemyState(EnemyState.Idle);
    }

    
    public void SetTarget(Transform t) => target = t;
    public void ClearTarget() => target = null;

    
    // 狀態機層
    protected virtual void SetEnemyState(EnemyState state)
    {
        enemyState = state;
    }
    public virtual void ActionState()
    {
        if (posture is not null && posture.isFull && enemyState != EnemyState.GuardBreak)
            SetEnemyState(EnemyState.GuardBreak);

        switch (enemyState)
        {
            case EnemyState.Idle: OnIdle(); break;

            case EnemyState.Chase: OnChase(); break;

            case EnemyState.Dash: OnDash(); break;
            
            case EnemyState.Attack: OnAttack(); break;

            case EnemyState.GuardBreak: OnGuardBreak(); break;
        }
    }
    public virtual void PhysicsState()
    {
        switch (enemyState)
        {
            case EnemyState.Chase:
                move.Move(faceDir);
                break;

            case EnemyState.GuardBreak:
                // GuardBreak 仍可移動（速度已在 OnGuardBreak 中調整）
                move.Move(faceDir);
                break;

            case EnemyState.Dash:
                dash.DashFixedUpdate();
                break;
        }
    }


    // ── 各狀態預設行為 ────────────────────────────────────────────────────
    protected virtual void OnIdle()
    {
        SetEnemyState(EnemyState.Chase);
    }
    protected virtual void OnChase()
    {
        UpdateFaceDir();
        attack.UpdateAttackDirection(faceDir);
        attack.CheckPlayerInFront();

        // Player 進入攻擊範圍 → 停止移動並切換至 Attack
        if (attack.IsTargetInRange(target.position))
        {
            SetEnemyState(EnemyState.Attack);
        }
    }
    protected virtual void OnDash()
    {
        if (!dash.IsDashing)
            SetEnemyState(EnemyState.Attack);
    }
    protected virtual void OnAttack()
    {
        attack.CheckPlayerInFront();

        if (attack.HasPlayerInFront) EnemyAttack();
        else SetEnemyState(EnemyState.Chase); // Player 離開範圍，重新追擊
    }
    protected virtual void OnGuardBreak()
    {
        UpdateFaceDir();
        attack.UpdateAttackDirection(faceDir);

        // 每幀維持 GuardBreak 移動速度
        float targetSpeed = posture is not null ? posture.guardSpeed : guardBreakMoveSpeed;
        move.Speed = targetSpeed;

        if (!isInGuardBreak)
        {
            isInGuardBreak = true;
            originalMoveSpeed = move.Speed;

            if (dash.IsDashing)
                dash.ForceStop();

            if (posture is not null)
                posture.ForceBroken();

            StartCoroutine(GuardBreakRoutine());
        }
    }
    protected virtual void EnemyAttack()
    {
        // 子類別實作具體攻擊行為
    }
    
    
    // ── 工具方法 ──────────────────────────────────────────────────────────
    protected void UpdateFaceDir()
    {
        if (target is null) return;
        faceDir = (target.position - transform.position).normalized;
    }
    protected float DistanceToTarget()
    {
        if (target is null) return float.MaxValue;
        
        return Vector2.Distance(transform.position, target.position);
    }
    protected void TryStartDash()
    {
        if (isInGuardBreak || enemyState == EnemyState.GuardBreak)
            return;

        UpdateFaceDir();
        if (dash.TryDash(faceDir))
        {
            SetEnemyState(EnemyState.Dash);
        }
    }

    IEnumerator GuardBreakRoutine()
    {
        yield return new WaitForSeconds(guardBreakDuration);
        move.Speed = originalMoveSpeed;
        isInGuardBreak = false;

        if (target is null)
            SetEnemyState(EnemyState.Idle);
        else
            SetEnemyState(EnemyState.Chase);
    }
}
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected MoveController move;
    protected EnemyAttack attack;
    protected DashController dash;
    [SerializeField] protected Transform target;
    
    
    protected Vector2 faceDir = Vector2.zero;
    protected EnemyState enemyState;

    
    [Header("Debug")] 
    [SerializeField] protected bool hasPlayerInFront;

    protected virtual void Awake()
    {
        move = GetComponent<MoveController>();
        attack = GetComponent<EnemyAttack>();
        dash = GetComponent<DashController>();
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
        switch (enemyState)
        {
            case EnemyState.Idle: OnIdle(); break;

            case EnemyState.Chase: OnChase(); break;

            case EnemyState.Dash: OnDash(); break;
            
            case EnemyState.Attack: OnAttack(); break;
        }
    }
    public virtual void PhysicsState()
    {
        switch (enemyState)
        {
            case EnemyState.Chase:
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
        UpdateFaceDir();
        if (dash.TryDash(faceDir))
        {
            SetEnemyState(EnemyState.Dash);
        }
    }
}
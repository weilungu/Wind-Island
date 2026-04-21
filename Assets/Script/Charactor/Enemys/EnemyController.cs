using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected MoveController move;
    protected AttackController attack;
    protected DashController dash;
    [SerializeField] protected StateMachine fsm;

    [SerializeField] protected Transform target;

    protected Vector2 faceDir = Vector2.zero;

    [Header("Debug")] 
    [SerializeField] protected bool hasPlayerInFront;

    protected virtual void Awake()
    {
        move = GetComponent<MoveController>();
        attack = GetComponent<AttackController>();
        dash = GetComponent<DashController>();
    }

    protected virtual void Start()
    {
        fsm.SetGameState(PlayerState.Idle);
    }

    public void SetTarget(Transform t) => target = t;
    public void ClearTarget() => target = null;

    // ── 狀態機（Update）──────────────────────────────────────────────────
    protected virtual void Update()
    {
        if (target == null) return;

        EnemyActionState();
    }

    // ── 狀態機（FixedUpdate）─────────────────────────────────────────────
    protected virtual void FixedUpdate()
    {
        if (target == null) return;

        EnemyPhysicsState();
    }

    // ── 邏輯層：子類別可覆寫個別 case ────────────────────────────────────
    protected virtual void EnemyActionState()
    {
        switch (fsm.enemyState)
        {
            case EnemyState.Idle:
                OnIdle();
                break;

            case EnemyState.Chase:
                OnChase();
                break;

            case EnemyState.Dash:
                OnDash();
                break;

            case EnemyState.Attack:
                OnAttack();
                break;
        }
    }

    // ── 物理層 ────────────────────────────────────────────────────────────
    protected virtual void EnemyPhysicsState()
    {
        switch (fsm.enemyState)
        {
            case EnemyState.Chase:
                move.Move(faceDir);
                break;

            case EnemyState.Dash:
                dash.DashFixedUpdate();
                break;
        }
    }

    // ── 各狀態預設行為（可被子類別 override）─────────────────────────────

    protected virtual void OnIdle()
    {
        // 預設：有 target 就直接開始追
        fsm.SetGameState(EnemyState.Chase);
    }

    protected virtual void OnChase()
    {
        UpdateFaceDir();
        attack.UpdateAttackDirection(faceDir);
    }

    protected virtual void OnDash()
    {
        // Dash 物理由 EnemyPhysicsState 驅動
        // 子類別負責決定 Dash 結束後切換到哪個狀態
        if (!dash.IsDashing)
        {
            fsm.SetGameState(PlayerState.Attack);
        }
    }

    protected virtual void OnAttack()
    {
        EnemyAttack();
    }

    protected virtual void EnemyAttack()
    {
        // 子類別實作具體攻擊行為
    }

    // ── 工具方法 ──────────────────────────────────────────────────────────
    protected void UpdateFaceDir()
    {
        if (target == null) return;
        faceDir = (target.position - transform.position).normalized;
    }

    protected float DistanceToTarget()
    {
        if (target == null) return float.MaxValue;
        return Vector2.Distance(transform.position, target.position);
    }
}
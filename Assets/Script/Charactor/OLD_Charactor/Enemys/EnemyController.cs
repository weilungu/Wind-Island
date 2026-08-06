using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected AudioSource audios;
    

    protected MoveController move;
    protected EnemyAttack attack;
    protected DashController dash;
    protected Health health;
    [SerializeField] protected Posture posture;
    [SerializeField] protected Transform target;
    
    
    protected Vector2 faceDir = Vector2.zero;
    protected Enemy_State enemyState;
    protected bool isInGuardBreak = false;
    protected float originalMoveSpeed = 0f;
    protected Coroutine hitStunRoutine;

    
    [Header("Debug")] 
    [SerializeField] protected bool hasPlayerInFront;
    
    [Header("GuardBreak")]
    [SerializeField] protected float guardBreakDuration = 0.5f;
    [SerializeField] protected float guardBreakMoveSpeed = 2f;
    
    [Header("Audio")]
    [SerializeField] protected AudioClip clipAttack;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audios = GetComponent<AudioSource>();

        move = GetComponent<MoveController>();
        attack = GetComponent<EnemyAttack>();
        dash = GetComponent<DashController>();
        health = GetComponent<Health>();
        if (posture is null)
            posture = GetComponent<Posture>();
    }
    protected virtual void OnEnable()
    {
        if (health is not null)
            health.OnDamaged += HandleDamaged;
        if (health is not null)
            health.OnDeath += HandleDeath;
    }

    protected virtual void OnDisable()
    {
        if (health is not null)
            health.OnDamaged -= HandleDamaged;
        if (health is not null)
            health.OnDeath -= HandleDeath;
    }
    protected virtual void Start()
    {
        SetEnemyState(Enemy_State.Idle);

        anim.SetFloat(AnimParams.MoveX, 0f);
        anim.SetFloat(AnimParams.MoveY, 0f);
    }

    
    public void SetTarget(Transform t) => target = t;
    public void ClearTarget() => target = null;

    
    // 狀態機層
    protected virtual void SetEnemyState(Enemy_State state)
    {
        enemyState = state;
    }
    public virtual void ActionState()
    {
        if (anim is null || move is null) return;

        if (posture is not null && posture.isFull && enemyState != Enemy_State.GuardBreak)
            SetEnemyState(Enemy_State.GuardBreak);

        switch (enemyState)
        {
            case Enemy_State.Idle: OnIdle(); break;

            case Enemy_State.Chase: OnChase(); break;

            case Enemy_State.Dash: OnDash(); break;
            
            case Enemy_State.Attack: OnAttack(); break;

            case Enemy_State.GuardBreak: OnGuardBreak(); break;

            case Enemy_State.HitStun: OnHitStun(); break;
        }
    }
    public virtual void PhysicsState()
    {
        if (move is null) return;

        switch (enemyState)
        {
            case Enemy_State.Chase:
                move.Move(faceDir);
                break;

            case Enemy_State.GuardBreak:
                // GuardBreak 仍可移動（速度已在 OnGuardBreak 中調整）
                move.Move(faceDir);
                break;

            case Enemy_State.Dash:
                dash.DashFixedUpdate();
                break;
        }
    }


    // ── 各狀態預設行為 ────────────────────────────────────────────────────
    protected virtual void OnIdle()
    {
        SetEnemyState(Enemy_State.Chase);
    }
    protected virtual void OnChase()
    {
        UpdateFaceDir();
        SetMoveAnim(faceDir);
        attack.UpdateAttackDirection(faceDir);
        attack.CheckPlayerInFront();

        // Player 進入攻擊範圍 → 停止移動並切換至 Attack
        if (attack.IsTargetInRange(target.position))
        {
            SetEnemyState(Enemy_State.Attack);
        }
    }
    protected virtual void OnDash()
    {
        SetMoveAnim(faceDir);
        if (!dash.IsDashing)
            SetEnemyState(Enemy_State.Attack);
    }
    protected virtual void OnAttack()
    {
        attack.CheckPlayerInFront();

        if (attack.HasPlayerInFront)
        {
            SetMoveAnim(faceDir);
            if (attack.canAttack) EnemyAttack();
        }
        else SetEnemyState(Enemy_State.Chase); // Player 離開範圍，重新追擊
    }
    protected virtual void OnGuardBreak()
    {
        UpdateFaceDir();
        SetMoveAnim(faceDir);
        attack.UpdateAttackDirection(faceDir);

        if (!isInGuardBreak)
        {
            isInGuardBreak = true;
            originalMoveSpeed = move.Speed;

            if (dash.IsDashing)
                dash.ForceStop();

            if (posture is not null)
                posture.StartGuardBreakRecover();

            StartCoroutine(GuardBreakRoutine());
        }

        // 每幀維持 GuardBreak 移動速度
        float targetSpeed = posture is not null ? posture.guardSpeed : guardBreakMoveSpeed;
        move.Speed = targetSpeed;
    }
    protected virtual void OnHitStun()
    {
        SetMoveAnim(Vector2.zero);
    }
    protected virtual void EnemyAttack()
    {
        PlayAudio(clipAttack);
    }
    
    
    // ── 工具方法 ──────────────────────────────────────────────────────────
    protected void PlayAudio(AudioClip clip)
    {
        if (clip is null) return;
        audios.PlayOneShot(clip);
    }
    protected void UpdateFaceDir()
    {
        if (target is null) return;
        faceDir = (target.position - transform.position).normalized;
    }
    protected void SetMoveAnim(Vector2 dir)
    {
        if (anim is null) return;
        anim.SetFloat(AnimParams.MoveX, dir.x);
        anim.SetFloat(AnimParams.MoveY, dir.y);
        if (sprite is not null && dir.x != 0f)
            sprite.flipX = dir.x < 0f;
    }
    protected float DistanceToTarget()
    {
        if (target is null) return float.MaxValue;
        
        return Vector2.Distance(transform.position, target.position);
    }
    protected void TryStartDash()
    {
        if (isInGuardBreak || enemyState == Enemy_State.GuardBreak || enemyState == Enemy_State.HitStun)
            return;

        UpdateFaceDir();
        if (dash.TryDash(faceDir))
        {
            SetEnemyState(Enemy_State.Dash);
        }
    }

    IEnumerator GuardBreakRoutine()
    {
        if (posture is not null)
        {
            while (posture.CurrentValue > posture.RecoverThreshold)
                yield return null;
        }
        else
        {
            yield return new WaitForSeconds(guardBreakDuration);
        }
        move.Speed = originalMoveSpeed;
        isInGuardBreak = false;

        if (posture is not null)
            posture.ContinueAfterGuardBreak();

        if (enemyState != Enemy_State.HitStun)
        {
            if (target is null)
                SetEnemyState(Enemy_State.Idle);
            else
                SetEnemyState(Enemy_State.Chase);
        }
    }

    void HandleDamaged(int damage)
    {
        if (enemyState == Enemy_State.GuardBreak)
            EnterHitStun();
    }

    void HandleDeath()
    {
        StopAllCoroutines();
        enemyState = Enemy_State.Dead;
        enabled = false;
        Destroy(gameObject);
    }

    void EnterHitStun()
    {
        if (hitStunRoutine is not null)
            StopCoroutine(hitStunRoutine);

        if (dash.IsDashing)
            dash.ForceStop();

        move.Speed = originalMoveSpeed;
        isInGuardBreak = false;
        if (posture is not null)
        {
            posture.ForceBroken();
            posture.SetIgnoreDamage(true);
        }

        SetEnemyState(Enemy_State.HitStun);
        hitStunRoutine = StartCoroutine(HitStunRoutine());
    }

    IEnumerator HitStunRoutine()
    {
        float duration = posture is not null ? posture.hitStunDuration : 0.5f;
        yield return new WaitForSeconds(duration);
        hitStunRoutine = null;
        if (posture is not null)
            posture.SetIgnoreDamage(false);

        if (target is null)
            SetEnemyState(Enemy_State.Idle);
        else
            SetEnemyState(Enemy_State.Chase);
    }
}
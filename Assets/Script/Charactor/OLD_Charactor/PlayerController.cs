using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Direction arguments
    private float horizontal;
    private float vertical;
    
    private Vector2 direction = Vector2.zero;
    private Vector2 faceDir = Vector2.right;

    // Instance
    private Animator anim;
    private SpriteRenderer sprite;
    private AudioSource audios;
    private Rigidbody2D rb;

    private InputController inp;
    private MoveController move;
    private DashController dash;
    private AttackController attack;
    private Health health;
    private Posture posture;
    
    private bool isInGuardBreak = false;
    private float originalMoveSpeed = 0f;
    private Coroutine hitStunRoutine;
    private bool isInHitStun = false;
    private bool isFalling = false;
    private Coroutine fallRoutine;
    private Vector3 respawnPosition;
    private Vector3 originalScale;

    public bool IsDead { get; private set; } = false;
    public event System.Action OnPlayerDead;
    

    [Header("Value")]
    [SerializeField] private float backlash = 10; // dash with posture
    
    [Header("Audio")]
    [SerializeField] private AudioClip clip_Move;
    
    [SerializeField] private AudioClip clip_Sword;
    [SerializeField] private AudioClip clip_Attack;
    [SerializeField] private AudioClip clip_Hurt;
    [SerializeField] private AudioClip clip_Dash;
    
    [SerializeField] private AudioClip clip_GuardBreak;
    [SerializeField] private AudioClip clip_HitStun;
    
    [Header("Debug")]
    [SerializeField] private Player_State playerState;

    [Header("Fall")]
    [SerializeField] private LayerMask fallZoneLayer;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float fallDuration = 0.6f;
    [SerializeField] private float fallDepth = 1.2f;

    void Awake()
    {
        inp = GetComponent<InputController>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audios = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
        health = GetComponent<Health>();
        attack = GetComponent<AttackController>();
        posture = GetComponent<Posture>();
    }

    void OnEnable()
    {
        if (health is not null)
        {
            health.OnDeath += HandleDeath;
            health.OnDamaged += HandleDamaged;
        }
        
    }

    void OnDisable()
    {
        if (health is not null)
        {
            health.OnDeath -= HandleDeath;
            health.OnDamaged -= HandleDamaged;
        }
    }

    void Start()
    {
        SetPlayerState(Player_State.Idle);
        anim.SetFloat(AnimParams.MoveX, 0f);
        anim.SetFloat(AnimParams.MoveY, 0f);
        anim.SetBool(AnimParams.IsMoving, false);
        originalMoveSpeed = move.Speed;
        originalScale = transform.localScale;
        respawnPosition = respawnPoint != null
            ? respawnPoint.position
            : transform.position;
    }

    void Update()
    {
        if (isFalling) return;
        inp.MoveInput(ref horizontal, ref vertical);
        direction = new Vector2(horizontal, vertical).normalized;
    }
    
    // 狀態機層
    void SetPlayerState(Player_State state)
    {
        playerState = state;
    }
    public void ActionState()
    {
        if (isFalling) return;
        switch (playerState)
        {
            case Player_State.Idle:
                SetMoveAnim(false);
        
                if (inp.movePressed)
                {
                    SetPlayerState(Player_State.Move);
                    break;
                }
        
                if (inp.attackPressed)
                {
                    SetPlayerState(Player_State.Attack);
                    break;
                }
        
                if (posture.isFull)
                {
                    SetPlayerState(Player_State.GuardBreak);
                    break;
                }
                
                if (inp.dashPressed && TryStartDash()) break; // → Dash state
                break;
        
            case Player_State.Move:
                ToMove();
                
                // Transition
                // 優先順序：Dash > Attack > Idle
                if (inp.dashPressed && TryStartDash()) break;
                if (inp.attackPressed)
                {
                    SetPlayerState(Player_State.Attack);
                    break;
                }
                if (posture.isFull)
                {
                    SetPlayerState(Player_State.GuardBreak);
                    break;
                }
        
                if (direction == Vector2.zero)
                {
                    SetPlayerState(Player_State.Idle);
                    break;
                }
        
                break;
        
            case Player_State.Dash:
                // Dash 結束由 DashController 回報，這裡只等待
                if (!dash.IsDashing)
                {
                    SetPlayerState(direction == Vector2.zero
                        ? Player_State.Idle
                        : Player_State.Move);
                }
                
                if (posture.isFull)
                {
                    SetPlayerState(Player_State.GuardBreak);
                    break;
                }
                break;
        
            case Player_State.Attack:
                if (attack.canAttack)
                {
                    anim.SetTrigger(AnimParams.Attack);
                    attack.TryAttack(faceDir);
                    PlayAudio(clip_Sword);
                    
                    if (attack.hitCount > 0)
                    {
                        posture.StartRecoveryRoutine(attack.hitCount * backlash);
                        
                        // Play Audio
                        PlayAudio(clip_Attack);
                    }
                    
                    attack.UpdateAttackDirection(faceDir);
                }
                else
                {
                    SetPlayerState(Player_State.Move);
                }
                break;
            
            
            case Player_State.GuardBreak:
                print("Guard Break");
                // GuardBreak 期間仍要依照輸入更新方向與移動動畫
                ToMove();

                // GuardBreak 期間仍可攻擊
                if (inp.attackPressed && attack.canAttack)
                {
                    anim.SetTrigger(AnimParams.Attack);

                    attack.TryAttack(faceDir);
                    PlayAudio(clip_Sword);
                    if (attack.hitCount > 0)
                    {
                        posture.StartRecoveryRoutine(attack.hitCount * backlash);
                        PlayAudio(clip_Attack);
                    }

                    attack.UpdateAttackDirection(faceDir);
                }
                
                // 只在剛進入 GuardBreak 時初始化（避免重複啟動協程與重覆設定速度）
                if (!isInGuardBreak)
                {
                    isInGuardBreak = true;
                    
                    // GuardBreak 只播放一次音效
                    PlayAudio(clip_GuardBreak);
                    // 記錄原本速度並降速
                    originalMoveSpeed = move.Speed;
                    move.Speed = posture.guardSpeed;

                    // 若 GuardBreak 打斷 Dash，強制結束 Dash，避免無法再次 Dash
                    if (dash.IsDashing)
                        dash.ForceStop();

                    posture.StartGuardBreakRecover();
                    StartCoroutine(GuardBreakRoutine());
                }
                break;
            
            
            case Player_State.HitStun:
                print("HitStun");
                // SetMoveAnim(false);
                break;
        }
    }
    public void PhysicsState()
    {
        if (isFalling) return;
        switch (playerState)
        {
            case Player_State.Move:
                move.Move(direction);
                attack.UpdateAttackDirection(direction);
                break;

            case Player_State.GuardBreak:
                // GuardBreak 仍可移動（速度已在 ActionState 中調整）
                move.Move(direction);
                attack.UpdateAttackDirection(direction);
                break;

            case Player_State.Dash:
                dash.DashFixedUpdate();
                print("Dash FixedUpdate");
                break;
        }
    }

    
    // ── 工具方法 ──────────────────────────────────────────────────────────
    void PlayAudio(AudioClip clip)
    {
        if (clip is null) return;
        audios.PlayOneShot(clip);
    }
    
    bool TryStartDash()
    {
        if (!dash.TryDash(direction)) return false;

        posture.StartDamageRoutine(backlash);
        anim.SetTrigger(AnimParams.Dash);
        PlayOneShot(clip_Dash);
        SetPlayerState(Player_State.Dash);

        return true;
    }

    void ToMove()
    {
        if (direction != Vector2.zero) faceDir = direction;
        
        if (direction.x != 0) sprite.flipX = direction.x < 0;
        SetMoveAnim(!direction.Equals(Vector2.zero));
    }
    void SetMoveAnim(bool isMoving)
    {
        anim.SetFloat(AnimParams.MoveX, faceDir.x);
        anim.SetFloat(AnimParams.MoveY, faceDir.y);
        anim.SetBool(AnimParams.IsMoving, isMoving);
    }

    IEnumerator GuardBreakRoutine()
    {
        while (posture.CurrentValue > posture.RecoverThreshold)
            yield return null;
        
        // 還原速度與狀態
        move.Speed = originalMoveSpeed;
        isInGuardBreak = false;
        posture.ContinueAfterGuardBreak();
        if (playerState != Player_State.HitStun)
            SetPlayerState(direction == Vector2.zero ? Player_State.Idle : Player_State.Move);
    }

    void HandleDamaged(int damage)
    {
        if (playerState == Player_State.GuardBreak)
            EnterHitStun();
    }

    void HandleDeath()
    {
        if (IsDead) return;

        IsDead = true;
        SetPlayerState(Player_State.Dead);
        OnPlayerDead?.Invoke();
    }

    void EnterHitStun()
    {
        if (hitStunRoutine is not null)
            StopCoroutine(hitStunRoutine);

        if (!isInHitStun)
        {
            isInHitStun = true;
            PlayOneShot(clip_HitStun);
        }

        if (dash.IsDashing)
            dash.ForceStop();

        move.Speed = originalMoveSpeed;
        isInGuardBreak = false;
        posture.ForceBroken();
        posture.SetIgnoreDamage(true);
        SetPlayerState(Player_State.HitStun);

        hitStunRoutine = StartCoroutine(HitStunRoutine());
    }

    IEnumerator HitStunRoutine()
    {
        yield return new WaitForSeconds(posture.hitStunDuration);
        hitStunRoutine = null;
        isInHitStun = false;
        posture.SetIgnoreDamage(false);
        SetPlayerState(direction == Vector2.zero ? Player_State.Idle : Player_State.Move);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDead || isFalling) return;

        if (((1 << other.gameObject.layer) & fallZoneLayer) == 0) return;

        EnterFall();
    }

    void EnterFall()
    {
        if (fallRoutine is not null)
            StopCoroutine(fallRoutine);

        isFalling = true;
        direction = Vector2.zero;

        if (dash.IsDashing)
            dash.ForceStop();

        SetMoveAnim(false);
        fallRoutine = StartCoroutine(FallRoutine());
    }

    IEnumerator FallRoutine()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.down * fallDepth;

        if (rb is not null)
            rb.velocity = Vector2.zero;

        while (elapsed < fallDuration)
        {
            float t = Mathf.Clamp01(elapsed / fallDuration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = respawnPosition;
        transform.localScale = originalScale;
        move.Speed = originalMoveSpeed;
        isInGuardBreak = false;
        isFalling = false;
        fallRoutine = null;
        SetPlayerState(Player_State.Idle);
    }

    void PlayOneShot(AudioClip clip)
    {
        if (clip is null) return;
        audios.PlayOneShot(clip);
    }
}
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

    private InputController inp;
    private MoveController move;
    private DashController dash;
    private AttackController attack;
    private Health health;
    private Posture posture;
    private bool isInGuardBreak = false;
    private float originalMoveSpeed = 0f;
    

    [Header("Value")]
    [SerializeField] private float backlash = 10; // dash with posture
    
    [Header("Debug")]
    [SerializeField] private PlayerState playerState;
    
    [Header("GuardBreak")]
    [SerializeField] private float guardBreakDuration = 0.5f;

    void Awake()
    {
        inp = GetComponent<InputController>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
        health = GetComponent<Health>();
        attack = GetComponent<AttackController>();
        posture = GetComponent<Posture>();
    }

    void Start()
    {
        SetPlayerState(PlayerState.Idle);
        anim.SetFloat(AnimParams.MoveX, 0f);
        anim.SetFloat(AnimParams.MoveY, 0f);
        anim.SetBool(AnimParams.IsMoving, false);
    }

    void Update()
    {
        inp.MoveInput(ref horizontal, ref vertical);
        direction = new Vector2(horizontal, vertical).normalized;
    }
    
    // 狀態機層
    void SetPlayerState(PlayerState state)
    {
        playerState = state;
    }
    public void ActionState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                SetMoveAnim(false);
        
                if (inp.movePressed)
                {
                    SetPlayerState(PlayerState.Move);
                    break;
                }
        
                if (inp.attackPressed)
                {
                    SetPlayerState(PlayerState.Attack);
                    break;
                }
        
                if (posture.isFull)
                {
                    SetPlayerState(PlayerState.GuardBreak);
                    break;
                }
                
                if (inp.dashPressed && TryStartDash()) break; // → Dash state
                break;
        
            case PlayerState.Move:
                ToMove();
                
                
                // Transition
                // 優先順序：Dash > Attack > Idle
                if (inp.dashPressed && TryStartDash()) break;
                if (inp.attackPressed)
                {
                    SetPlayerState(PlayerState.Attack);
                    break;
                }
                if (posture.isFull)
                {
                    SetPlayerState(PlayerState.GuardBreak);
                    break;
                }
        
                if (direction == Vector2.zero)
                {
                    SetPlayerState(PlayerState.Idle);
                    break;
                }
        
                break;
        
            case PlayerState.Dash:
                // Dash 結束由 DashController 回報，這裡只等待
                if (!dash.IsDashing)
                {
                    SetPlayerState(direction == Vector2.zero
                        ? PlayerState.Idle
                        : PlayerState.Move);
                }
                
                if (posture.isFull)
                {
                    SetPlayerState(PlayerState.GuardBreak);
                    break;
                }
                break;
        
            case PlayerState.Attack:
                if (attack.canAttack)
                {
                    anim.SetTrigger(AnimParams.Attack);
        
                    attack.TryAttack(faceDir);
                    if (attack.hitCount > 0)
                    {
                        StartCoroutine(posture.Recovery(attack.hitCount * backlash));
                    }
                    
                    attack.UpdateAttackDirection(faceDir);
                }
                else
                {
                    SetPlayerState(PlayerState.Move);
                }
                break;
            
            
            case PlayerState.GuardBreak:
                print("Guard Break");
                // 只在剛進入 GuardBreak 時初始化（避免重複啟動協程與重覆設定速度）
                if (!isInGuardBreak)
                {
                    isInGuardBreak = true;
                    // 記錄原本速度並降速
                    originalMoveSpeed = move.Speed;
                    move.Speed = posture.guardSpeed;

                    // 若 GuardBreak 打斷 Dash，強制結束 Dash，避免無法再次 Dash
                    if (dash.IsDashing)
                        dash.ForceStop();

                    posture.ForceBroken();
                    // 讓角色仍能移動（較慢）並播放對應動畫
                    ToMove();

                    StartCoroutine(GuardBreakRoutine());
                }
                break;
            
            
            case PlayerState.HitStun:
                break;
        }
    }
    public void PhysicsState()
    {
        switch (playerState)
        {
            case PlayerState.Move:
                move.Move(direction);
                attack.UpdateAttackDirection(direction);
                break;

            case PlayerState.GuardBreak:
                // GuardBreak 仍可移動（速度已在 ActionState 中調整）
                move.Move(direction);
                attack.UpdateAttackDirection(direction);
                break;

            case PlayerState.Dash:
                dash.DashFixedUpdate();
                print("Dash FixedUpdate");
                break;
        }
    }

    
    // ── 工具方法 ──────────────────────────────────────────────────────────
    bool TryStartDash()
    {
        if (!dash.TryDash(direction)) return false;

        StartCoroutine(posture.TakePostureDamage(backlash));
        anim.SetTrigger(AnimParams.Attack);
        SetPlayerState(PlayerState.Dash);

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
        yield return new WaitForSeconds(guardBreakDuration);
        // 還原速度與狀態
        move.Speed = originalMoveSpeed;
        isInGuardBreak = false;
        SetPlayerState(direction == Vector2.zero ? PlayerState.Idle : PlayerState.Move);
    }
}
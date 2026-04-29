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

    // [Header("Field Instance")]
    // [SerializeField] private StateMachine fsm;

    [Header("Value")]
    [SerializeField] private float postureValue = 10;
    
    private PlayerState playerState;

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
    private void SetPlayerState(PlayerState state)
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
        
                if (inp.dashPressed && TryStartDash()) break; // → Dash state
                break;
        
            case PlayerState.Move:
                if (!direction.Equals(Vector2.zero)) faceDir = direction;
        
                if (direction.x != 0) sprite.flipX = direction.x < 0;
                SetMoveAnim(!direction.Equals(Vector2.zero));
        
                // 優先順序：Dash > Attack > Idle
                if (inp.dashPressed && TryStartDash()) break;
                if (inp.attackPressed)
                {
                    SetPlayerState(PlayerState.Attack);
                    break;
                }
        
                if (direction.Equals(Vector2.zero))
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
                break;
        
            case PlayerState.Attack:
                if (attack.canAttack)
                {
                    anim.SetTrigger(AnimParams.Attack);
        
                    attack.TryAttack(faceDir);
                    if (attack.hitCount > 0)
                    {
                        StartCoroutine(posture.Recovery(attack.hitCount * postureValue));
                    }
                    
                    attack.UpdateAttackDirection(faceDir);
                }
                else
                {
                    SetPlayerState(PlayerState.Move);
                }
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

            case PlayerState.Dash:
                dash.DashFixedUpdate();
                break;
        }
    }

    
    // ── 工具方法 ──────────────────────────────────────────────────────────
    bool TryStartDash()
    {
        if (!dash.TryDash(direction)) return false;

        StartCoroutine(posture.TakePostureDamage(postureValue));
        anim.SetTrigger(AnimParams.Attack);
        SetPlayerState(PlayerState.Dash);

        return true;
    }
    void SetMoveAnim(bool isMoving)
    {
        anim.SetFloat(AnimParams.MoveX, faceDir.x);
        anim.SetFloat(AnimParams.MoveY, faceDir.y);
        anim.SetBool(AnimParams.IsMoving, isMoving);
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector2 direction = Vector2.zero;
    Vector2 faceDir = Vector2.right;

    // Instance
    Animator anim;
    SpriteRenderer sprite;

    InputController inp;
    MoveController move;
    DashController dash;
    AttackController attack;
    Health health;
    Posture posture;

    [Header("Field Instance")]
    [SerializeField] StateMachine fsm;

    [Header("Value")]
    [SerializeField] float postureValue = 10;

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
        fsm.SetGameState(PlayerState.Idle);
        anim.SetFloat(AnimParams.MoveX, 0f);
        anim.SetFloat(AnimParams.MoveY, 0f);
        anim.SetBool(AnimParams.IsMoving, false);
    }

    void Update()
    {
        inp.MoveInput(ref horizontal, ref vertical);
        direction = new Vector2(horizontal, vertical).normalized;
    }
    

    // ── 狀態機：邏輯層（Update）──────────────────────────────────────────
    public void ActionState()
    {
        switch (fsm.playerState)
        {
            case PlayerState.Idle:
                SetMoveAnim(false);
        
                if (inp.movePressed)
                {
                    fsm.SetGameState(PlayerState.Move);
                    break;
                }
        
                if (inp.attackPressed)
                {
                    fsm.SetGameState(PlayerState.Attack);
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
                    fsm.SetGameState(PlayerState.Attack);
                    break;
                }
        
                if (direction.Equals(Vector2.zero))
                {
                    fsm.SetGameState(PlayerState.Idle);
                    break;
                }
        
                break;
        
            case PlayerState.Dash:
                // Dash 結束由 DashController 回報，這裡只等待
                if (!dash.IsDashing)
                {
                    fsm.SetGameState(direction.Equals(Vector2.zero)
                        ? PlayerState.Idle
                        : PlayerState.Move);
                }
        
                break;
        
            case PlayerState.Attack:
                if (attack.canAttack)
                {
                    anim.SetTrigger(AnimParams.Attack);
        
                    attack.TryAttack(faceDir);
                    attack.UpdateAttackDirection(faceDir);
                }
                else
                {
                    fsm.SetGameState(PlayerState.Move);
                }
        
                break;
        
            case PlayerState.Hurt:
                health.TakeDamage(10);
                posture.TakePostureDamage(10);
                fsm.SetGameState(PlayerState.Idle);
                break;
        }
    }

    // ── 狀態機：物理層（FixedUpdate）─────────────────────────────────────
    public void PhysicsState()
    {
        switch (fsm.playerState)
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

        fsm.SetGameState(PlayerState.Dash);
        posture.TakePostureDamage(postureValue);
        anim.SetTrigger(AnimParams.Attack);

        return true;
    }

    void SetMoveAnim(bool isMoving)
    {
        anim.SetFloat(AnimParams.MoveX, faceDir.x);
        anim.SetFloat(AnimParams.MoveY, faceDir.y);
        anim.SetBool(AnimParams.IsMoving, isMoving);
    }
}
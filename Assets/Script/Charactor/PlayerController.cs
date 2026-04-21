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

    void Awake()
    {
        inp    = GetComponent<InputController>();
        anim   = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
        move   = GetComponent<MoveController>();
        dash   = GetComponent<DashController>();
        health = GetComponent<Health>();
        attack = GetComponent<AttackController>();
        posture = GetComponent<Posture>();
    }

    void Start()
    {
        fsm.SetGameState(GameState.Idle);
        anim.SetFloat(AnimParams.MoveX, 0f);
        anim.SetFloat(AnimParams.MoveY, 0f);
        anim.SetBool(AnimParams.IsMoving, false);
    }

    void Update()
    {
        inp.MoveInput(ref horizontal, ref vertical);
        direction = new Vector2(horizontal, vertical).normalized;

        PlayerActionState();
    }

    void FixedUpdate()
    {
        PhysicsState();
    }

    // ── 狀態機：邏輯層（Update）──────────────────────────────────────────
    void PlayerActionState()
    {
        switch (fsm.gameState)
        {
            case GameState.Idle:
                SetMoveAnim(false);

                if (inp.movePressed)                  { fsm.SetGameState(GameState.Move);   break; }
                if (inp.attackPressed)                { fsm.SetGameState(GameState.Attack); break; }
                if (Input.GetMouseButtonDown(1))      { fsm.SetGameState(GameState.Hurt);   break; }
                if (inp.dashPressed && TryStartDash()) break;   // → Dash state
                break;

            case GameState.Move:
                if (!direction.Equals(Vector2.zero)) faceDir = direction;

                if (direction.x != 0) sprite.flipX = direction.x < 0;
                SetMoveAnim(!direction.Equals(Vector2.zero));

                // 優先順序：Dash > Attack > Idle
                if (inp.dashPressed && TryStartDash()) break;
                if (inp.attackPressed) { fsm.SetGameState(GameState.Attack); break; }
                if (direction.Equals(Vector2.zero))    { fsm.SetGameState(GameState.Idle);  break; }
                break;

            case GameState.Dash:
                // Dash 結束由 DashController 回報，這裡只等待
                if (!dash.IsDashing)
                {
                    fsm.SetGameState(direction.Equals(Vector2.zero)
                        ? GameState.Idle 
                        : GameState.Move);
                }
                break;

            case GameState.Attack:
                if (attack.canAttack)
                {
                    anim.SetTrigger(AnimParams.Attack);
                    
                    attack.TryAttack(faceDir);
                    attack.UpdateAttackDirection(faceDir);
                }
                else
                {
                    fsm.SetGameState(GameState.Move);
                }
                break;

            case GameState.Hurt:
                health.TakeDamage(10);
                posture.TakePosture(10);
                fsm.SetGameState(GameState.Idle);
                break;
        }
    }

    // ── 狀態機：物理層（FixedUpdate）─────────────────────────────────────
    void PhysicsState()
    {
        switch (fsm.gameState)
        {
            case GameState.Move:
                move.Move(direction);
                attack.UpdateAttackDirection(direction);
                break;

            case GameState.Dash:
                dash.DashFixedUpdate();
                break;
        }
    }

    // ── 工具方法 ──────────────────────────────────────────────────────────
    bool TryStartDash()
    {
        if (!dash.TryDash(direction)) return false;
        
        fsm.SetGameState(GameState.Dash);
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
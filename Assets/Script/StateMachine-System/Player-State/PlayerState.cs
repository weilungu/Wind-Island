using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] string animationName;
    int animationHash;
    
    protected PlayerStateMachine stateMachine;
    protected PlayerStateContext ctx;
    
    
    // Contexts
    protected Animator anim;
    protected SpriteRenderer sprite;
    
    protected New_PlayerController player;
    protected PlayerInput input;
    
    protected PlayerMove move;
    protected PlayerDash dash;
    protected PlayerPosture posture;
    protected PlayerGuardBreak guardBreak;
    protected PlayerCombat combat;

    // Can Be Something
    protected virtual bool CanBe_Attack => true;
    
    void OnEnable()
    {
        animationHash = Animator.StringToHash(animationName);
    }
    
    public void Initialize(PlayerStateMachine stateMachine, PlayerStateContext ctx)
    {
        this.stateMachine = stateMachine;
        this.ctx = ctx;
    }
    
    public virtual void Enter()
    {
        // Init Contexts
        GetContexts();
        
        // Init Logics
        anim.Play(animationHash);
        Debug.Log(animationName);
    }

    public virtual void Exit() {}

    public virtual void LogicalUpdate()
    {
        if (CanBe_Attack && input.Attack)
            combat.Attack();
    }

    public virtual void PhysicalUpdate() {}
    
    
    // Contexts
    void GetContexts()
    {
        anim = ctx.Get<Animator>();
        sprite = ctx.Get<SpriteRenderer>();
        
        player = ctx.Get<New_PlayerController>();
        input = ctx.Get<PlayerInput>();
        
        move = ctx.Get<PlayerMove>();
        dash = ctx.Get<PlayerDash>();
        posture = ctx.Get<PlayerPosture>();
        guardBreak = ctx.Get<PlayerGuardBreak>();
        combat = ctx.Get<PlayerCombat>();
    }
}
